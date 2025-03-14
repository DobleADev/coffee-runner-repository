using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance { get; private set; }
    public int coins;
    public int premiumCoins;
    public List<LevelProgressData> levelProgress = new List<LevelProgressData>();  

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

    public void CompleteLevel(string levelName)
    {
        LevelProgressData levelData = levelProgress.FirstOrDefault(level => level.levelName == levelName);
        if (levelData != null)
        {
            levelData.completed = true;
        }
        else
        {
            levelProgress.Add(new LevelProgressData{levelName = levelName, completed = true});
        }
    }

    public LevelProgressData GetLevelProgress(string levelName)
    {
        return levelProgress.FirstOrDefault(level => level.levelName == levelName);
    }
}
