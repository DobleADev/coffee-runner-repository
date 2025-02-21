
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onStart;
    void Start()
    {
        _onStart.Invoke();
    }

}
