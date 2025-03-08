using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStatusEffect", menuName = "Scriptable Objects/Player Status Effect")]
public class PlayerStatusEffectSO : ScriptableObject
{
    public string effectName;
    [TextArea] public string description;
    public EffectType type;
    [Min(1)] public int level = 1;
    public FloatUpgradable duration;
    public FloatUpgradable useCooldown;
    public List<StatusEffectProperty> properties = new List<StatusEffectProperty>();
    public enum EffectType { Active, Permanent }
}

public interface IUpgradable<T>
{
    T baseValue { get; set; }
    UpgradeData[] upgrades { get; set; }
    T Value(int level);
}

[System.Serializable]
public struct UpgradeData
{
    public float setting;
    public bool percentage;
    public float Value(float baseValue) 
    { 
        return percentage ? (baseValue * setting * 0.01f) : setting; 
    }
}

[System.Serializable]
public struct FloatUpgradable : IUpgradable<float>
{
    [SerializeField] float _baseValue;
    [SerializeField] UpgradeData[] _upgrades;
    public float baseValue { get { return _baseValue; } set { _baseValue = value; } }
    public UpgradeData[] upgrades { get { return _upgrades; } set { _upgrades = value; } }
    public float Value(int level)
    {
        return level > 1 ? _baseValue + _upgrades[level-2].Value(_baseValue) : _baseValue;
    }
}


public abstract class StatusEffectProperty : ScriptableObject
{
    public virtual void Apply(PlayerController player) {}
    public virtual void Remove(PlayerController player) {}
    public virtual void EachFrame(PlayerController player) {}

}

public abstract class StatusUpgradableEffectProperty : StatusEffectProperty
{
    public abstract FloatUpgradable upgrades { get; set; }
}