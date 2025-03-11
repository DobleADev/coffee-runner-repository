using UnityEngine;

[CreateAssetMenu(fileName = "NewTemperatureChangeProperty", menuName = "Scriptable Objects/Effect Properties/Temperature Change")]
public class TemperatureChangeStatusEffectSO : StatusUpgradableEffectProperty
{
    public bool basePercentage;
    public TemperatureCondition conditionType;
    public float condition;
    public bool conditionPercentage;
    [SerializeField] FloatUpgradable _upgrades;
    private float GetTemperature(float entityTemperature) => basePercentage ? (entityTemperature * _upgrades.Value(level) * 0.01f) : _upgrades.Value(level);
    private float GetCondition(float entityTemperature) => basePercentage ? (entityTemperature * condition * 0.01f) : condition;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }
    public override void Apply(PlayerController player)
    {
        float playerTemperature = player.Temperature;
        switch (conditionType)
        {
            case TemperatureCondition.LessThan:
            {
                if (playerTemperature >= GetCondition(playerTemperature))
                {
                    return;
                }
            } break;

            case TemperatureCondition.MoreThan:
            {
                if (playerTemperature <= GetCondition(playerTemperature))
                {
                    return;
                }
            } break;
        }
        player.ApplyTemperature(GetTemperature(playerTemperature));
    }
}

public enum TemperatureCondition
{
    None, 
    LessThan,
    MoreThan
}