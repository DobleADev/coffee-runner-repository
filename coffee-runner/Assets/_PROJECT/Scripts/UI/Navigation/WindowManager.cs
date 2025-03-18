using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private Stack<Window> windows = new Stack<Window>();

    public void OpenWindow(Window window)
    {
        OpenWindow(window, null);
    }

    public void OpenWindow(Window window, object parameters = null)
    {
        if (windows.Count > 0)
        {
            windows.Peek().Close();
        }
        window.Open(parameters);
        windows.Push(window);
    }

    public void CloseLastWindow()
    {
        if (windows.Count > 0)
        {
            Window window = windows.Pop();
            window.Close();

            if (windows.Count > 0)
            {
                windows.Peek().Open();
            }
        }
    }

    public void CloseAllWindows()
    {
        while (windows.Count > 0)
        {
            Window window = windows.Pop();
            window.Close();
        }
        windows.Clear();

    }
}
