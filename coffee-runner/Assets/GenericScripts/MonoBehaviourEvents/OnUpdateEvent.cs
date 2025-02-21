
using UnityEngine;
using UnityEngine.Events;

public class OnUpdateEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onUpdate;
    void Update()
    {
        _onUpdate.Invoke();
    }
}