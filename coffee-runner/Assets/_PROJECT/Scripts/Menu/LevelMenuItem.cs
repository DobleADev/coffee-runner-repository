using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuItem : MonoBehaviour
{

    [SerializeField] private TMP_Text label;
    [SerializeField] private Button levelButton;
    [SerializeField] private GameObject detailsObject;
    [SerializeField] private GameObject lockedObject;
    private LevelDataSO level;
    private LevelMenuManager window;

    public void SetUp(LevelDataSO level, LevelMenuManager window)
    {
        this.level = level;
        this.window = window;
        label.text = level.levelName;
    }

    public void SetLock(bool locked)
    {
        levelButton.interactable = !locked;
        detailsObject.SetActive(!locked);
        lockedObject.SetActive(locked);
    }

    public void OnClick()
    {
        window.SelectWorld(level);
    }
}
