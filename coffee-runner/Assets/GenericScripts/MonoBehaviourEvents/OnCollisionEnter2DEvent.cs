
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEnter2DEvent : MonoBehaviour
{
    [SerializeField] string targetTag = "Untagged";
    [SerializeField] UnityEvent _onCollisionEnter2D;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!other.gameObject.CompareTag(targetTag)) return;
        Raise();
    }

    public void Raise()
    {
        if (_onCollisionEnter2D == null) return;
        _onCollisionEnter2D.Invoke();
    }
}
