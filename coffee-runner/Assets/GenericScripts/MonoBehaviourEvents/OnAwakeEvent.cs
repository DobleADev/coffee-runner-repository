
using UnityEngine;
using UnityEngine.Events;

public class OnAwakeEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onAwake;
    void Awake()
    {
        _onAwake.Invoke();
    }
}
