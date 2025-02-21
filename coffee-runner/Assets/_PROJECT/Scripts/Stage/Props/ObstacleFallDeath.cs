using UnityEngine;

public class ObstacleFallDeath : ObstacleInstakill
{
    public override void DealDeath(PlayerController entity)
    {
        entity.DeathEvents.onFallDeath?.Invoke();
    }
}
