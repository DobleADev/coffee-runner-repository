
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onDestroy;
    void OnDestroy()
    {
        Raise();
    }

    public void Raise()
    {
        _onDestroy.Invoke();
    }
}
