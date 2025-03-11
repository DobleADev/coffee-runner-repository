using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStatusEffect", menuName = "Scriptable Objects/Player Status Effect")]
public class PlayerStatusEffectSO : ScriptableObject
{
    public string effectName;
    [TextArea] public string description;
    public EffectType type;
    [SerializeField, Min(1)] private int _level = 1;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            UpdatePropertiesLevel();
        }
    }
    [Min(1)] public int maxLevel = 1;
    public FloatUpgradable duration;
    public FloatUpgradable useCooldown;
    public List<StatusEffectProperty> properties = new List<StatusEffectProperty>();
    public enum EffectType { Active, Permanent }

    void OnValidate()
    {
        UpdatePropertiesLevel();
    }

    void UpdatePropertiesLevel()
    {
        foreach (var effect in properties)
        {
            effect.level = _level;
        }
    }
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
        if (_upgrades.Length == 0) return _baseValue;
        if (level > 1)
        {
            UpgradeData upgrade = _upgrades[Mathf.Min(level - 2, _upgrades.Length - 1)];
            return _baseValue + upgrade.Value(_baseValue);
        }
        return _baseValue;
    }
}


public abstract class StatusEffectProperty : ScriptableObject
{
    public virtual int level { get; set; }
    public virtual void Apply(PlayerController player) { }
    public virtual void Remove(PlayerController player) { }
    public virtual void EachFrame(PlayerController player) { }

}

public abstract class StatusUpgradableEffectProperty : StatusEffectProperty
{
    [SerializeField, Min(1)] private int _level = 1;
    public override int level { get => _level; set => _level = value; }
    public abstract FloatUpgradable upgrades { get; set; }
}