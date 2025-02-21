using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoostEffect", menuName = "Scriptable Objects/Player Effect/Speed Boost Effect")]
public class SpeedBoostPlayerEffectSO : DurationEffectSO
{
    public float speedBoost;
    public override void Apply(PlayerController player)
    {
        player.AddSpeed(speedBoost);
    }

    public override void Remove(PlayerController player)
    {
        player.RemoveSpeed(speedBoost);
    }
}
