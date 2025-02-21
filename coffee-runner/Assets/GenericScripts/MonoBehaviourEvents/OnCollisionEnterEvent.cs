
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEnterEvent : MonoBehaviour
{
    [SerializeField] string targetTag = "Untagged";
    [SerializeField] UnityEvent _onCollisionEnter;
    private void OnCollisionEnter(Collision other) 
    {
        if (!other.gameObject.CompareTag(targetTag)) return;
        Raise();
    }

    public void Raise()
    {
        if (_onCollisionEnter == null) return;
        _onCollisionEnter.Invoke();
    }
}
