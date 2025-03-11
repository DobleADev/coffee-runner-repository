using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    [SerializeField] private LoadSceneMode loadSceneMode;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }
}
