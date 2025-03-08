using UnityEngine;

public class ObstacleChangeTemperature : ObstacleBase
{
    [SerializeField] private float _temperature;

    protected override void OnPlayerCrash(PlayerController player)
    {
        // int playerInvencibilityCount = player.InvencibilityChances.Count;
        // if (playerInvencibilityCount > 0)
        // {
        //     var lastChance = player.InvencibilityChances[playerInvencibilityCount - 1];
        //     lastChance.Remove(player);
        //     return;
        // }
        player.BufferedTemperature += _temperature;
    }
}
