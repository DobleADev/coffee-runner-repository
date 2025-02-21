using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour, IInteractable<PlayerController>
{
    [SerializeField] PlayerEffectSO[] _effects;
    [SerializeField] protected UnityEvent _onInteract;

    public virtual void Interact(PlayerController interactor)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            interactor.ApplyEffect(_effects[i]);
        }
        _onInteract?.Invoke();
    }
}

