using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewPlayerStatusEffect", menuName = "Scriptable Objects/Player Status Effect")]
public class PlayerStatusEffectSO : ScriptableObject
{
    [SerializeField, FormerlySerializedAs("effectName")] private string _effectName;
    public string effectName { get => _effectName + " Lv." + _level; }
    [TextArea] public string description;
    public EffectType type;
    [SerializeField, Min(1)] private int _level = 1;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            _level = Mathf.Clamp(_level, 1, maxLevel);
            UpdatePropertiesLevel();
        }
    }
    [Min(1)] public int maxLevel = 1;
    public FloatUpgradable duration;
    public FloatUpgradable useCooldown;
    public List<StatusEffectProperty> properties = new List<StatusEffectProperty>();
    public enum EffectType { Active, Permanent }


    public string GetUpgradeDescription()
    {
        StringBuilder description = new StringBuilder();
        if (level < maxLevel)
        {
            if (level < duration.upgrades.Length+1) description.AppendLine(duration.upgrades.Length > 0 ? "Duration: " + duration.Value(level) + " -> " + duration.Value(level+1) : "No Upgrades");
            if (level < useCooldown.upgrades.Length+1) description.AppendLine(useCooldown.upgrades.Length > 0 ? "Cooldown: " + useCooldown.Value(level) + " -> " + useCooldown.Value(level+1) : "No Upgrades");
            foreach (var property in properties)
            {
                if (property is not StatusUpgradableEffectProperty) continue;
                var upgradeableProperty = (StatusUpgradableEffectProperty) property;
                string upgradeDescription = upgradeableProperty.GetUpgradeDescription();
                if (upgradeDescription != "") description.AppendLine(upgradeDescription);
            }
        }
        else
        {
            description.AppendLine("MAX LEVEL");
        }

        return description.ToString();
    }

    [ContextMenu("Reset level")]
    void ResetLevel()
    {
        level = 0;
    }

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

public interface IUpgradable<T> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
{
    T baseValue { get; set; }
    public UpgradeData<T>[] upgrades { get; set; }
    T Value(int level);
}

[System.Serializable]
public struct UpgradeData<T> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
{
    public T setting;
    public bool percentage;

    public T Value(T baseValue)
    {
        if (percentage)
        {
            // Convert to double for percentage calculation, then back to T.
            double settingDouble = Convert.ToDouble(setting);
            double baseValueDouble = Convert.ToDouble(baseValue);
            double resultDouble = baseValueDouble * settingDouble * 0.01;
            return (T)Convert.ChangeType(resultDouble, typeof(T));
        }
        else
        {
            return setting;
        }
    }

    // Example of a constructor that takes a generic parameter:
    public UpgradeData(T setting, bool percentage)
    {
        this.setting = setting;
        this.percentage = percentage;
    }

    //Example of a constructor that takes a double and converts it to T
    public UpgradeData(double setting, bool percentage)
    {
        this.setting = (T)Convert.ChangeType(setting, typeof(T));
        this.percentage = percentage;
    }

    //Example of a constructor that takes an int and converts it to T
    public UpgradeData(int setting, bool percentage)
    {
        this.setting = (T)Convert.ChangeType(setting, typeof(T));
        this.percentage = percentage;
    }
}

[System.Serializable]
public struct FloatUpgradable : IUpgradable<float>
{
    [SerializeField] float _baseValue;
    [SerializeField] UpgradeData<float>[] _upgrades;
    public float baseValue { get { return _baseValue; } set { _baseValue = value; } }
    public UpgradeData<float>[] upgrades { get { return _upgrades; } set { _upgrades = value; } }

    public float Value(int level)
    {
        if (_upgrades.Length == 0) return _baseValue;
        if (level > 1)
        {
            UpgradeData<float> upgrade = _upgrades[Mathf.Min(level - 2, _upgrades.Length - 1)];
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
    public virtual string effectName => "effect";

    public string GetUpgradeDescription()
    {
        string description = "";
        if (level < upgrades.upgrades.Length+1) description = upgrades.upgrades.Length > 0 ? effectName + ": " + upgrades.Value(level) + " -> " + upgrades.Value(level+1) : "No Upgrades";
        return description;
    }
}