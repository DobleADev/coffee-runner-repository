using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance { get; private set; }
    public int coins;
    public int premiumCoins;
    public List<LevelProgressData> levelProgress = new List<LevelProgressData>();  
    // public List<WorldProgressData> worldProgress = new List<WorldProgressData>();  
    public GameObject currentPlayerSkin;
    public List<GameObject> ownedSkins;
    [SerializeField] List<PlayerStatusEffectOwned> _powerups = new List<PlayerStatusEffectOwned>();  

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

    // public WorldProgressData GetWorldProgress(string worldName)
    // {
    //     return worldProgress.FirstOrDefault(world => world.worldName == worldName);
    // }

    public bool isWorldCompleted(WorldDataSO world)
    {
        bool completed = true;
        foreach (var level in world.levels)
        {
            var worldLevel = levelProgress.Find(levelProgress => levelProgress.levelName == level.levelName);
            if (worldLevel == null)
            {
                completed = false;
                break;
            }
            
            if (!worldLevel.completed)
            {
                completed = false;
                break;
            }
            // if (!level.isCompleted())
            // {
            //     completed = false;
            //     break;
            // }
        }
        return completed;
    }

    public void AddOwnedPowerup(PlayerStatusEffectSO powerup)
    {
        PlayerStatusEffectOwned ownedPowerup = _powerups.Find(ownedPowerup => ownedPowerup.statusEffect == powerup);
        if (ownedPowerup != null)
        {
            ownedPowerup.quantity ++;
            return;
        }
        
        ownedPowerup = new PlayerStatusEffectOwned();
        ownedPowerup.statusEffect = powerup;
        ownedPowerup.quantity = 1;
        _powerups.Add(ownedPowerup);
    }

    public List<PlayerStatusEffectSO> EquipOwnedPowerups()
    {
        List<PlayerStatusEffectSO> result = new List<PlayerStatusEffectSO>();
        for (int i = _powerups.Count - 1; i >= 0 ; i--)
        {
            var powerup = _powerups[i];
            if (powerup.isEquipped)
            {
                if (powerup.quantity > 1)
                {
                    powerup.quantity --;
                }
                else
                {
                    _powerups.Remove(powerup);
                }
                result.Add(powerup.statusEffect);
            }
        }
        return result;
    }

    public PlayerStatusEffectOwned GetOwnedPowerup(int index)
    {
        return _powerups[index];
    }

    public PlayerStatusEffectOwned GetOwnedPowerup(PlayerStatusEffectSO powerup)
    {
        return _powerups.Find(ownedPowerup => ownedPowerup.statusEffect == powerup);
    }

    public int GetOwnedPowerupsCount()
    {
        return _powerups.Count;
    }

    public void AddOwnedPrefab(GameObject prefab)
    {
        ownedSkins.Add(prefab);
    }
}
