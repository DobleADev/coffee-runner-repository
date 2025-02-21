
using UnityEngine;
using UnityEngine.Events;

public class OnDisableEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onDisable;
    void OnDisable()
    {
        Raise();
    }

    public void Raise()
    {
        _onDisable.Invoke();
    }
}
