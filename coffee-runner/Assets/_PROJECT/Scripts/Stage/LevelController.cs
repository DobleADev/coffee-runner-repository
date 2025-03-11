using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] PlayerSpawn _start;
    [SerializeField] Transform _end;
    [SerializeField] List<PlayerStatusEffectSO> _environmentDefaultEffects = new List<PlayerStatusEffectSO>();

    public PlayerSpawn start => _start;
    public Transform end => _end;
    public List<PlayerStatusEffectSO> EnvironmentDefaultEffects => _environmentDefaultEffects;

    void OnValidate()
    {
        for (int i = _environmentDefaultEffects.Count - 1; i >= 0 ; i--)
        {
            var effect = _environmentDefaultEffects[i];
            if (effect == null) continue;
            if (effect.type != PlayerStatusEffectSO.EffectType.Permanent)
            {
                _environmentDefaultEffects.Remove(effect);
            }
        }
    }
}
