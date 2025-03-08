using UnityEngine;

[CreateAssetMenu(fileName = "NewSlipShieldProperty", menuName = "Scriptable Objects/Effect Properties/Slip Shield")]
public class SlipShieldStatusEffectSO : StatusEffectProperty
{
    [SerializeField] GameObject _visualPrefab;
    private GameObject _visual;

    public override void Apply(PlayerController player)
    {
        if (_visual != null)
        {
            return;
        }
        _visual = Instantiate(_visualPrefab, player.transform);
        player.CurrentSlipShields.Add(this);
    }

    public void Use(PlayerController player)
    {
        if (_visual == null) return;
        player.InvencibilityTime = 2f;
        Remove(player);
    }

    public override void Remove(PlayerController player)
    {
        if (_visual == null)
        {
            return;
        }
        Destroy(_visual);
        _visual = null;
        player.CurrentSlipShields.Remove(this);
    }
}
