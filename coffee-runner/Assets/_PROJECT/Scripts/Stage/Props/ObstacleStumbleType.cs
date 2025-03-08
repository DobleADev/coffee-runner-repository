using UnityEngine;

public class ObstacleStumbleType : ObstacleInstakill
{
    public override void DealDeath(PlayerController entity)
    {
        int shieldCount = entity.CurrentStumbleShields.Count;
        if (shieldCount > 0)
        {
            var shield = entity.CurrentStumbleShields[shieldCount - 1];
            shield.Use(entity);
            return;
        }

        entity.DeathEvents.onStumbleDeath?.Invoke();
    }
}
