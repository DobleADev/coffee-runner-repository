using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnter2DEvent : MonoBehaviour
{
    [SerializeField] string targetTag = "Untagged";
    [SerializeField] UnityEvent _onTriggerEnter2D;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.gameObject.CompareTag(targetTag)) return;
        Raise();
    }

    public void Raise()
    {
        if (_onTriggerEnter2D == null) return;
        _onTriggerEnter2D.Invoke();
    }
}