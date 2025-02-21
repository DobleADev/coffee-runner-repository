using UnityEngine;

[CreateAssetMenu(fileName = "NewTemperatureChangeEffect", menuName = "Scriptable Objects/Player Effect/Temperature Change Effect")]
public class TemperatureChangePlayerEffectSO : InstantEffectSO
{
    public float temperature;
    public override void Apply(PlayerController player)
    {
        player.ApplyTemperature(temperature);
    }
}
