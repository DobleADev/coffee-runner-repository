using UnityEngine;

[CreateAssetMenu(fileName = "NewInvencibilityProperty", menuName = "Scriptable Objects/Effect Properties/Invencibility")]
public class InvencibilityStatusEffectSO : StatusUpgradableEffectProperty
{
    bool _active = false;
    [SerializeField] FloatUpgradable _upgrades;
    public override FloatUpgradable upgrades { get => _upgrades; set => _upgrades = value; }

    public override void Apply(PlayerController player)
    {
        if (_active) return;
        _active = true;
        player.InvencibilityChances.Add(this);
    }

    public override void Remove(PlayerController player)
    {
        if (!_active) return;
        _active = false;
        player.InvencibilityTime = 2f;
        player.InvencibilityChances.Remove(this);
    }
}
