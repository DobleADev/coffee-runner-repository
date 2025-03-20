using UnityEngine;

[CreateAssetMenu(fileName = "NewTemperatureChangeEffect", menuName = "Scriptable Objects/Effect Properties/Environment Temperature")]
public class EnvironmentTemperatureSettingStatusEffectSO : StatusUpgradableEffectProperty
{
    public float temperature;
    [SerializeField] FloatUpgradable _upgrades;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }
    public override string effectName => "Environment Temperature";
 
    public override void Apply(PlayerController player)
    {
        player.EnvironmentTemperature += temperature;
    }

    public override void Remove(PlayerController player)
    {
        player.EnvironmentTemperature -= temperature;
    }
}
