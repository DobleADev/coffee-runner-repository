using UnityEngine;

public class ObstacleSlipType : ObstacleInstakill
{
    public override void DealDeath(PlayerController entity)
    {
        int shieldCount = entity.CurrentSlipShields.Count;
        if (shieldCount > 0)
        {
            var shield = entity.CurrentSlipShields[shieldCount - 1];
            shield.Remove(entity);
            return;
        }

        entity.DeathEvents.onSlipDeath?.Invoke();
    }
}
