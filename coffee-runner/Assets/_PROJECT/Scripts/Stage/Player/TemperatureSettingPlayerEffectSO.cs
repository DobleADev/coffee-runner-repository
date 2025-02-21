using UnityEngine;

[CreateAssetMenu(fileName = "NewTemperatureChangeEffect", menuName = "Scriptable Objects/Player Effect/Temperature Setting Effect")]
public class TemperatureSettingPlayerEffectSO : PermanentEffectSO
{
    public float temperature;
    public override void Apply(PlayerController player)
    {
        player.ApplyTemperature(temperature, Time.deltaTime);
    }
}
