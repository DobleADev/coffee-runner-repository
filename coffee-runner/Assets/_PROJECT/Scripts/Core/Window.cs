using System;
using UnityEngine;

public class Window : MonoBehaviour
{
    public event Action OnWindowOpened;
    public event Action OnWindowClosed;

    public virtual void Open(object parameters = null)
    {
        gameObject.SetActive(true);
        OnWindowOpened?.Invoke();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        OnWindowClosed?.Invoke();
    }
}
