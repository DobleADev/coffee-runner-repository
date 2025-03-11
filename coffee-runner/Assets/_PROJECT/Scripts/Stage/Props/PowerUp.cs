using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour, IInteractable<PlayerController>
{
    [SerializeField] TMP_Text _text;
    [SerializeField] PlayerStatusEffectSO _effect;
    [SerializeField] protected UnityEvent _onInteract;

    public virtual void Interact(PlayerController interactor)
    {
        if (_effect == null) return;
        interactor.ApplyEffect(_effect);
        _onInteract?.Invoke();
    }

    void OnValidate()
    {
        if (_text == null)
        {
            return;
        }
        _text.text = _effect == null ? "Empty shell" : _effect.effectName;
    }
}

