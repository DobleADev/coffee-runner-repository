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

    public void OpenWindow(Window window)
    {
        WindowManager.instance.OpenWindow(window);
    }

    public void CloseLastWindow()
    {
        WindowManager.instance.CloseLastWindow();
    }

    public void CloseAllWindows()
    {
        WindowManager.instance.CloseAllWindows();
    }
}
