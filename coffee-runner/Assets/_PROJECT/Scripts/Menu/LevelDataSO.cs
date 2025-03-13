using UnityEngine;

[CreateAssetMenu(fileName = "newLevelData", menuName = "Scriptable Objects/Level/Data")]
public class LevelDataSO : ScriptableObject
{
    public string levelName;
    public LevelController prefab;
    public bool completed;

    // public enum UnlockRequirement { completion }

    public void SetData(LevelDataSO data)
    {
        levelName = data.levelName;
        prefab = data.prefab;
    }
}
