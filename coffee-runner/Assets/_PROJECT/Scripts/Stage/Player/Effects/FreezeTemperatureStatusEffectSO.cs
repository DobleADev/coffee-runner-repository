using UnityEngine;

[CreateAssetMenu(fileName = "NewFreezeTemperatureStatusEffectProperty", menuName = "Scriptable Objects/Effect Properties/Freeze temperature")]
public class FreezeTemperatureStatusEffectSO : StatusEffectProperty
{
    public override void EachFrame(PlayerController player)
    {
        player.CapTemperature();
    }
}
