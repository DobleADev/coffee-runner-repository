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

    protected virtual void OnPlayerCrash(PlayerController player) { }
}

public abstract class ObstacleInstakill : ObstacleBase, IDeathStrategy<PlayerController>
{
    [SerializeField] bool _forceDeath = false;
    public abstract void DealDeath(PlayerController entity);

    protected override void OnPlayerCrash(PlayerController player)
    {
        int playerInvencibilityCount = player.InvencibilityChances.Count;
        if (playerInvencibilityCount > 0 && !_forceDeath)
        {
            var lastChance = player.InvencibilityChances[playerInvencibilityCount - 1];
            lastChance.Use(player);
            return;
        }

        if (player.InvencibilityTime > 0 && !_forceDeath)
        {
            return;
        }
        DealDeath(player);
    }
}