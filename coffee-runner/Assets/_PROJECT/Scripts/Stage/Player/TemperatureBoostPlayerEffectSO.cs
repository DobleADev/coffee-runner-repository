using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTemperatureBoostEffect", menuName = "Scriptable Objects/Player Effect/Temperature Boost Effect")]
public class TemperatureBoostPlayerEffectSO : DurationEffectSO
{
    public float temperatureChangeRate;

    public override void Update(PlayerController player)
    {
        player.ApplyTemperature(temperatureChangeRate, Time.deltaTime);
    }
}
