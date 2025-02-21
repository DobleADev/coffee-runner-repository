using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            OnPlayerCrash(player);
        }
    }

    protected virtual void OnPlayerCrash(PlayerController player) {}
}

public abstract class ObstacleInstakill : ObstacleBase, IDeathStrategy<PlayerController>
{
    public abstract void DealDeath(PlayerController entity);

    protected override void OnPlayerCrash(PlayerController player)
    {
        DealDeath(player);
    }
}