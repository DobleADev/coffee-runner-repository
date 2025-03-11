using System;
using TMPro;
using UnityEngine;

public class LevelMenuItem : MonoBehaviour
{

    [SerializeField] private TMP_Text label;
    [SerializeField] private LevelDataSO level;
    [SerializeField] private LevelMenuManager window;

    public void SetUp(LevelDataSO level, LevelMenuManager window)
    {
        this.level = level;
        this.window = window;
        label.text = level.levelName;
    }

    public void OnClick()
    {
        window.SelectWorld(level);
    }
}
