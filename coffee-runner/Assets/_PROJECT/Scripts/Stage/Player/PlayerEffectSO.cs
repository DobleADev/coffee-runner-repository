using UnityEngine;

public abstract class PlayerEffectSO : ScriptableObject
{
    public string effectName;
    public virtual void Apply(PlayerController player) {}
}

public abstract class InstantEffectSO : PlayerEffectSO {}

public abstract class DurationEffectSO : PlayerEffectSO
{
    public float duration;
    public virtual void Remove(PlayerController player) {}
    public virtual void Update(PlayerController player) {}
}

public abstract class PermanentEffectSO : DurationEffectSO {}



[System.Serializable]
public class ActiveStatusEffect
{
    public DurationEffectSO effect;
    public float timeRemaining;

    public ActiveStatusEffect(DurationEffectSO effect)
    {
        this.effect = effect;
        timeRemaining = effect.duration;
    }

    public void UpdateEffect(PlayerController player)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            effect.Update(player);
        }
        else
        {
            timeRemaining = 0;
            player.RemoveEffect(this);
        }
    }

    public void Reset()
    {
        timeRemaining = effect.duration;
    }
}