using UnityEngine;

[CreateAssetMenu(fileName = "NewStumbleShieldProperty", menuName = "Scriptable Objects/Effect Properties/Stumble Shield")]
public class StumbleShieldStatusEffectSO : StatusEffectProperty
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
        player.CurrentStumbleShields.Add(this);
    }

    public override void Remove(PlayerController player)
    {
        if (_visual == null)
        {
            return;
        }
        Destroy(_visual);
        player.InvencibilityTime = 2f;
        player.CurrentStumbleShields.Remove(this);
    }
}
