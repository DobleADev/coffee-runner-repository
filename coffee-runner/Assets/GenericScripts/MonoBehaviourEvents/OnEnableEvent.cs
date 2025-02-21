using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onEnable;
    void OnEnable()
    {
        Raise();
    }

    public void Raise()
    {
        _onEnable.Invoke();
    }
}
