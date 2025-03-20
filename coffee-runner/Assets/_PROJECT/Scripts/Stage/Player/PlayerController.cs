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
    [SerializeField] private List<SlipShieldStatusEffectSO> _currentSlipShields = new List<SlipShieldStatusEffectSO>();
    [SerializeField] private List<StumbleShieldStatusEffectSO> _currentStumbleShields = new List<StumbleShieldStatusEffectSO>();
    [SerializeField] bool _doubleJump;
    [SerializeField] private List<ActiveEffect> _activeEffects = new List<ActiveEffect>();
    [SerializeField] private List<PlayerStatusEffectSO> _activePermanentEffects = new List<PlayerStatusEffectSO>();
    [SerializeField] private UnityEvent _onRevive;
    [SerializeField] private UnityEvent _onInvencibilityEnd;
    [SerializeField] private PlayerDeathEvents _deathEvents;
    #endregion

    #region Private Variables
    private float _currentRunSpeed;
    float _currentTemperature;
    float _currentInvencibilityTime;
    List<InvencibilityStatusEffectSO> _currentInvencibilityChances = new List<InvencibilityStatusEffectSO>();
    float _currentBufferedTemperature;
    bool _isTemperatureCap;
    Coroutine _invencibilityCoroutine;
    #endregion

    #region Properties
    public float BaseSpeed => _baseRunSpeed;
    public float RunSpeed => _currentRunSpeed;
    public float Temperature => _currentTemperature;
    public float EffectRunSpeed { get; private set; }
    public float EnvironmentTemperature { get; set; }
    public float EffectTemperature { get; private set; }
    public float BufferedTemperature { get { return _currentBufferedTemperature; } set { _currentBufferedTemperature = value; } }
    public float InvencibilityTime { get { return _currentInvencibilityTime; } set { _currentInvencibilityTime = value; } }
    public List<InvencibilityStatusEffectSO> InvencibilityChances { get { return _currentInvencibilityChances; } set { _currentInvencibilityChances = value; } }
    public List<SlipShieldStatusEffectSO> CurrentSlipShields { get { return _currentSlipShields; } set { _currentSlipShields = value; } }
    public List<StumbleShieldStatusEffectSO> CurrentStumbleShields { get { return _currentStumbleShields; } set { _currentStumbleShields = value; } }
    public List<ActiveEffect> activeEffects => _activeEffects;
    public List<PlayerStatusEffectSO> activePermanentEffects => _activePermanentEffects;
    public PlayerDeathEvents DeathEvents { get { return _deathEvents; } set { _deathEvents = value; } }
    // public List<ActiveEffect> activeEffects = new List<ActiveEffect>();

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

        foreach (var effect in _activeEffects)
        {
            foreach (var effectData in effect.effect.properties)
            {
                effectData.EachFrame(this);
            }
        }

        foreach (var effect in _activePermanentEffects)
        {
            foreach (var effectData in effect.properties)
            {
                effectData.EachFrame(this);
            }
        }
        if (!_isTemperatureCap)
        {
            _currentBufferedTemperature += (EffectTemperature + EnvironmentTemperature) * Time.deltaTime;
        }

        _currentInvencibilityTime -= Time.deltaTime;
        _currentInvencibilityTime = Mathf.Max(0, _currentInvencibilityTime);

        _physics.moveSpeed = _currentRunSpeed;
        _physics.maxAirJumps = _doubleJump ? 1 : 0;
        UpdateActiveEffectDurations(); // Actualiza las duraciones de los efectos activos 
    }

    void LateUpdate()
    {
        _isTemperatureCap = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable<PlayerController> interactable))
        {
            interactable.Interact(this);
        }
    }
    #endregion

    #region Public Methods
    public void AddSpeed(float amount) { EffectRunSpeed += amount; }
    public void RemoveSpeed(float amount) { EffectRunSpeed -= amount; }
    public void AddTemperature(float amount) { EffectTemperature += amount; }
    public void RemoveTemperature(float amount) { EffectTemperature -= amount; }
    public void ApplyTemperature(float amount, float delta = 1)
    {
        if (_isTemperatureCap)
        {
            return;
        }
        _currentBufferedTemperature += delta * amount;
    }
    public void CapTemperature() { _isTemperatureCap = true; }

    private void UpdateActiveEffectDurations()
    {
        List<ActiveEffect> effectsToRemove = new List<ActiveEffect>();

        for (int i = 0; i < _activeEffects.Count; i++)
        {
            ActiveEffect activeEffect = _activeEffects[i];
            activeEffect.remainingTime -= Time.deltaTime;
            _activeEffects[i] = activeEffect; // Actualiza el struct en la lista

            if (activeEffect.remainingTime <= 0)
            {
                effectsToRemove.Add(activeEffect);
            }
        }

        foreach (ActiveEffect effectToRemove in effectsToRemove)
        {
            // Lógica para remover el efecto del jugador (si es necesario)
            foreach (var effectData in effectToRemove.effect.properties)
            {
                effectData.Remove(this);
            }
            _activeEffects.Remove(effectToRemove);
        }
    }

    public void ApplyEffect(PlayerStatusEffectSO effect)
    {

        // Manejar la duración si el efecto es activo
        switch (effect.type)
        {
            case PlayerStatusEffectSO.EffectType.Active:
                {
                    ActiveEffect activeEffect = _activeEffects.Find(activeEffect => activeEffect.effect == effect);
                    if (activeEffect.effect != null)
                    {
                        int id = _activeEffects.IndexOf(activeEffect);
                        activeEffect.remainingTime = effect.duration.Value(effect.level);
                        _activeEffects[id] = activeEffect;
                        if (effect.properties != null)
                        {
                            foreach (var effectData in effect.properties)
                            {
                                effectData.Remove(this);
                                effectData.Apply(this); // Llama al método Apply de la propiedad
                            }
                        }
                        return;
                    }
                    ActiveEffect newActiveEffect = new ActiveEffect
                    {
                        effect = effect,
                        remainingTime = effect.duration.Value(effect.level)
                    };
                    if (effect.properties != null)
                    {
                        foreach (var effectData in effect.properties)
                        {
                            effectData.Apply(this); // Llama al método Apply de la propiedad
                        }
                    }
                    _activeEffects.Add(newActiveEffect);
                }
                break;
            case PlayerStatusEffectSO.EffectType.Permanent:
                {
                    if (effect.properties != null)
                    {
                        foreach (var effectData in effect.properties)
                        {
                            effectData.Apply(this); // Llama al método Apply de la propiedad
                        }
                    }
                    _activePermanentEffects.Add(effect);
                }
                break;
        }
    }

    public void RemoveEffect(PlayerStatusEffectSO effect)
    {
        switch (effect.type)
        {
            case PlayerStatusEffectSO.EffectType.Active:
                {
                    var activeEffect = _activeEffects.Find(activeEffect => activeEffect.effect == effect);
                    if (activeEffect.effect == null)
                    {
                        return;
                    }
                    _activeEffects.Remove(activeEffect);
                }
                break;
            case PlayerStatusEffectSO.EffectType.Permanent:
                {
                    if (!_activePermanentEffects.Contains(effect)) return;
                    _activePermanentEffects.Remove(effect);
                }
                break;
        }

        // Debug.Log(effect.effectName + " removed");

        foreach (var effectData in effect.properties)
        {
            effectData.Remove(this);
        }
        // activeEffects.RemoveAll(activeEffect => activeEffect.effect == effect); // Remueve el efecto de la lista
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
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activeEffects[i];
            // if (effect == null) continue;
            RemoveEffect(effect.effect);
        }

        for (int i = _activePermanentEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activePermanentEffects[i];
            // if (effect == null) continue;
            RemoveEffect(effect);
        }
        // for (int i = 0; i < _activePermanentEffects.Count; i++)
        // {
        //     var effect = _activePermanentEffects[i];
        //     // if (effect == null) continue;
        //     foreach (var effectData in effect.effects)
        //     {
        //         float finalValue = effectData.baseValue;
        //         effectData.property.Remove(this);
        //     }
        // }
        EnvironmentTemperature = 0;
        EffectTemperature = 0;
        _currentBufferedTemperature = 0;
        _currentRunSpeed = _baseRunSpeed + EffectRunSpeed;
        _currentTemperature = _baseTemperature + _currentBufferedTemperature;
        // var reviveInvencibility = ScriptableObject.CreateInstance<InvencibilityStatusEffectSO>();
        // reviveInvencibility.Apply(this);
        _currentInvencibilityTime = _reviveInvencibiltyCooldown;
        gameObject.SetActive(true);
        // if (_invencibilityCoroutine != null) StopCoroutine(_invencibilityCoroutine);
        // _invencibilityCoroutine = StartCoroutine(InvencibilityCoroutine(reviveInvencibility));
    }

    // IEnumerator InvencibilityCoroutine(InvencibilityStatusEffectSO effect)
    // {
    //     yield return new WaitForSeconds(_reviveInvencibiltyCooldown);
    //     effect.Remove(this);
    // }
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

[System.Serializable]
public struct ActiveEffect
{
    public PlayerStatusEffectSO effect;
    public float remainingTime;
}