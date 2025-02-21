using UnityEngine;

public class ObstacleStumbleType : ObstacleInstakill
{
    public override void DealDeath(PlayerController entity)
    {
        entity.DeathEvents.onStumbleDeath?.Invoke();
    }
}
