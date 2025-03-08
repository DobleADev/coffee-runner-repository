using System.Collections.Generic;
using UnityEngine;

public class LevelZone : MonoBehaviour
{
    [SerializeField] List<PlayerStatusEffectSO> _zoneEffects = new List<PlayerStatusEffectSO>();
    public List<PlayerStatusEffectSO> zoneEffects => _zoneEffects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            var levelEffects = _zoneEffects;
            for (int i = 0; i < levelEffects.Count; i++)
            {
                var effect = levelEffects[i];
                if (effect == null) continue;
                player.ApplyEffect(effect);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            var levelEffects = _zoneEffects;
            for (int i = 0; i < levelEffects.Count; i++)
            {
                var effect = levelEffects[i];
                if (effect == null) continue;
                player.RemoveEffect(effect);
            }
        }
    }

    void OnValidate()
    {
        for (int i = _zoneEffects.Count - 1; i >= 0 ; i--)
        {
            var effect = _zoneEffects[i];
            if (effect == null) continue;
            if (effect.type != PlayerStatusEffectSO.EffectType.Permanent)
            {
                _zoneEffects.Remove(effect);
            }
        }
    }
}
