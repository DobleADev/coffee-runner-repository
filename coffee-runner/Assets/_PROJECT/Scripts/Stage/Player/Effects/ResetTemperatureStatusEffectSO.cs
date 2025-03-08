using UnityEngine;

[CreateAssetMenu(fileName = "NewResetTemperatureProperty", menuName = "Scriptable Objects/Effect Properties/Reset Temperature")]
public class ResetTemperatureStatusEffectSO : StatusEffectProperty
{
    public override void Apply(PlayerController player)
    {
        player.EffectTemperature = 0;
    }
}
