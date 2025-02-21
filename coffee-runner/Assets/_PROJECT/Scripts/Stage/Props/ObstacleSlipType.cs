using UnityEngine;

public class ObstacleSlipType : ObstacleInstakill
{
    public override void DealDeath(PlayerController entity)
    {
        entity.DeathEvents.onSlipDeath?.Invoke();
    }
}
