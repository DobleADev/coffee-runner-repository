using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoostEffectProperty", menuName = "Scriptable Objects/Effect Properties/Temperature Boost")]
public class TemperatureBoostStatusEffectSO : StatusUpgradableEffectProperty
{
    private float GetTemperature() => _upgrades.Value(level) * 0.01f;
    [SerializeField] FloatUpgradable _upgrades;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }

    public override void Apply(PlayerController player)
    {
        player.AddTemperature(GetTemperature());
    }

    public override void Remove(PlayerController player)
    {
        player.RemoveTemperature(GetTemperature());
    }
}
