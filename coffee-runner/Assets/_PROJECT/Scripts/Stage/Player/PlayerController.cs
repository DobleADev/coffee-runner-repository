using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region Serialized Variables
    [Header("Dependencies")]
    [SerializeField] SimpleRunnerPhysics _physics;
    // [SerializeField] Animator _animator;
    [Header("Properties")]
    [SerializeField] private float _baseRunSpeed = 4f;
    [SerializeField] private float _baseTemperature = 0;
    [SerializeField] private float _heatSupported = 100;
    [SerializeField] private float _coldSupported = -100;
    [SerializeField] private float _reviveInvencibiltyCooldown = 2;
    [SerializeField] bool _doubleJump;
    [SerializeField] private List<ActiveStatusEffect> _activeEffects = new List<ActiveStatusEffect>();
    [SerializeField] private List<PermanentEffectSO> _activePermanentEffects = new List<PermanentEffectSO>();
    [SerializeField] private UnityEvent _onRevive;
    [SerializeField] private UnityEvent _onInvencibilityEnd;
    [SerializeField] private PlayerDeathEvents _deathEvents;
    #endregion

    #region Private Variables
    private float _currentRunSpeed;
    float _currentTemperature;
    float _currentInvencibilityTime;
    float _currentBufferedTemperature;
    bool _isTemperatureCap;
    Coroutine _invencibilityCoroutine;
    #endregion

    #region Properties
    public float RunSpeed => _currentRunSpeed;
    public float Temperature => _currentTemperature;
    public float EffectRunSpeed { get; private set; }
    public float EffectTemperature { get { return _currentBufferedTemperature; } set {_currentBufferedTemperature = value;} }
    public float InvencibilityTime { get { return _currentInvencibilityTime; } set {_currentInvencibilityTime = value;} }
    public List<ActiveStatusEffect> activeEffects => _activeEffects;
    public List<PermanentEffectSO> activePermanentEffects => _activePermanentEffects;
    public PlayerDeathEvents DeathEvents { get { return _deathEvents; } set { _deathEvents = value; } }
    #endregion

    #region Private Methods
    void Update()
    {
        _currentRunSpeed = _baseRunSpeed + EffectRunSpeed;
        _currentTemperature = _baseTemperature + _currentBufferedTemperature;
        if (_currentTemperature >= _heatSupported)
        {
            _currentTemperature = _heatSupported;
            _deathEvents.onCookedDeath?.Invoke();
        }
        else if (_currentTemperature < _coldSupported)
        {
            _currentTemperature = _coldSupported;
            _deathEvents.onFrozedDeath?.Invoke();
        }

        for (int i = 0; i < _activeEffects.Count; i++)
        {
            var effect = _activeEffects[i];
            // if (effect == null) continue;
            effect.UpdateEffect(this);
        }

        for (int i = 0; i < _activePermanentEffects.Count; i++)
        {
            var effect = _activePermanentEffects[i];
            // if (effect == null) continue;
            effect.Apply(this);
        }

        foreach (var effect in _activePermanentEffects)
        {
            effect.Apply(this); // Se aplican en cada frame
        }
        
        _physics.moveSpeed = _currentRunSpeed;
        _physics.maxAirJumps = _doubleJump ? 1 : 0;
    }

    void LateUpdate()
    {
        _isTemperatureCap = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PowerUp powerUp))
        {
            powerUp.Interact(this);
        }
    }
    #endregion

    #region Public Methods
    public void AddSpeed(float amount) { EffectRunSpeed += amount; }
    public void RemoveSpeed(float amount) { EffectRunSpeed -= amount; }
    public void ApplyTemperature(float amount, float delta = 1) 
    { 
        if (_isTemperatureCap)
        {
            return;
        }
        _currentBufferedTemperature += delta * amount; 
    }
    public void CapTemperature() { _isTemperatureCap = true; }

    public void ApplyEffect(PlayerEffectSO effect)
    {
        if (effect is PermanentEffectSO) // TEMP
        {
            _activePermanentEffects.Add((PermanentEffectSO) effect);
        }
        else if (effect is DurationEffectSO)
        {
            ActiveStatusEffect existingEffect = _activeEffects.Find(activeEffect => activeEffect.effect == effect);

            if (existingEffect != null)
            {
                existingEffect.Reset();
            }
            else
            {
                var newActiveEffect = new ActiveStatusEffect((DurationEffectSO) effect);
                effect.Apply(this);
                _activeEffects.Add(newActiveEffect);
            }
            
        }
    }

    public void RemoveEffect(ActiveStatusEffect activeEffect)
    {
        if (!_activeEffects.Contains(activeEffect)) return;
        var effect = activeEffect.effect;
        effect.Remove(this);
        _activeEffects.Remove(activeEffect);
    }

    public void RemoveEffect(PermanentEffectSO permanentEffect)
    {
        if (!_activePermanentEffects.Contains(permanentEffect)) return;
        permanentEffect.Remove(this);
        _activePermanentEffects.Remove(permanentEffect);
    }

    public void ValidateDeath()
    {
        _deathEvents.onAnyDeath?.Invoke();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    public void Revive()
    {
        for (int i = 0; i < _activeEffects.Count; i++)
        {
            var effect = _activeEffects[i];
            // if (effect == null) continue;
            RemoveEffect(effect);
        }
        for (int i = 0; i < _activePermanentEffects.Count; i++)
        {
            var effect = _activePermanentEffects[i];
            // if (effect == null) continue;
            RemoveEffect(effect);
        }
        _currentBufferedTemperature = 0;
        _currentRunSpeed = _baseRunSpeed + EffectRunSpeed;
        _currentTemperature = _baseTemperature + _currentBufferedTemperature;
        _currentInvencibilityTime = _reviveInvencibiltyCooldown;
        gameObject.SetActive(true);
        _invencibilityCoroutine = StartCoroutine(InvencibilityCoroutine());
    }

    IEnumerator InvencibilityCoroutine()
    {
        yield return new WaitForSeconds(_reviveInvencibiltyCooldown);
        _currentInvencibilityTime = 0;
    }
    #endregion
}

[System.Serializable]
public struct PlayerDeathEvents
{
    public UnityEvent onAnyDeath;
    public UnityEvent onCookedDeath;
    public UnityEvent onFrozedDeath;
    public UnityEvent onFallDeath;
    public UnityEvent onSlipDeath;
    public UnityEvent onStumbleDeath;
    public UnityEvent onHandOffDeath;
}
