using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWorldData", menuName = "Scriptable Objects/World/Data")]
public class WorldDataSO : ScriptableObject
{
    public string worldName;
    public Sprite worldImage;
    public int starsRequired;
    public List<LevelDataSO> levels = new List<LevelDataSO>();

    public void SetData(WorldDataSO data) 
    { 
        worldName = data.worldName;
        worldImage = data.worldImage;
        starsRequired = data.starsRequired;
        levels = data.levels;
    }
}
