using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoostEffectProperty", menuName = "Scriptable Objects/Effect Properties/Temperature Boost")]
public class TemperatureBoostStatusEffectSO : StatusUpgradableEffectProperty
{
    public float temperatureChangeRate;
    [SerializeField] FloatUpgradable _upgrades;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }

    public override void Apply(PlayerController player)
    {
        player.AddTemperature(temperatureChangeRate);
    }

    public override void Remove(PlayerController player)
    {
        player.RemoveTemperature(temperatureChangeRate);
    }
}
