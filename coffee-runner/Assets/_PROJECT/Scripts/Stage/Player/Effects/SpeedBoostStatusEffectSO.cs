using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoostEffectProperty", menuName = "Scriptable Objects/Effect Properties/Speed Boost")]
public class SpeedBoostStatusEffectSO : StatusUpgradableEffectProperty
{
    public bool basePercentage;
    private float GetBoost(float entitySpeed) => basePercentage ? (entitySpeed * _upgrades.Value(level) * 0.01f) : _upgrades.Value(level);
    [SerializeField] FloatUpgradable _upgrades;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }

    public override void Apply(PlayerController player)
    {
        player.AddSpeed(GetBoost(player.BaseSpeed));
    }

    public override void Remove(PlayerController player)
    {
        player.RemoveSpeed(GetBoost(player.BaseSpeed));
    }
}
