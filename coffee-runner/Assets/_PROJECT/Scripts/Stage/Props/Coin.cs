using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInteractable<PlayerController>
{
    [SerializeField] protected UnityEvent _onInteract;

    public virtual void Interact(PlayerController interactor)
    {
        GameDataManager.instance.coins ++;
        _onInteract?.Invoke();
    }
}
