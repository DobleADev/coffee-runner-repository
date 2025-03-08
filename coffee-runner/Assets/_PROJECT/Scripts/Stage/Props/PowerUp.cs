using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour, IInteractable<PlayerController>
{
    [SerializeField] PlayerStatusEffectSO[] _effects;
    [SerializeField] protected UnityEvent _onInteract;

    public virtual void Interact(PlayerController interactor)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            var effect = _effects[i];
            if (effect == null) continue;
            interactor.ApplyEffect(effect);
        }
        _onInteract?.Invoke();
    }
}

