using UnityEngine;

[CreateAssetMenu(fileName = "NewInvencibilityProperty", menuName = "Scriptable Objects/Effect Properties/Invencibility")]
public class InvencibilityStatusEffectSO : StatusEffectProperty
{
    bool _active = false;

    public override void Apply(PlayerController player)
    {
        if (_active) return;
        _active = true;
        player.InvencibilityChances.Add(this);
    }

    public void Use(PlayerController player)
    {
        if (!_active) return;
        player.InvencibilityTime = 2f;
        Remove(player);
    }

    public override void Remove(PlayerController player)
    {
        if (!_active) return;
        _active = false;
        player.InvencibilityChances.Remove(this);
    }
}
