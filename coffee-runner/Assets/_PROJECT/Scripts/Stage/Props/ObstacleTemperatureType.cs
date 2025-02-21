using UnityEngine;

public class ObstacleChangeTemperature : ObstacleBase
{
    [SerializeField] private float _temperature;

    protected override void OnPlayerCrash(PlayerController player)
    {
        player.EffectTemperature += _temperature;
    }
}
