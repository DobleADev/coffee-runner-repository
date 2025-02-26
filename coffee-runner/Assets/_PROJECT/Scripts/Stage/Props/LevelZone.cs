using UnityEngine;

public class LevelZone : MonoBehaviour
{
    [SerializeField] PermanentEffectSO[] _zoneEffects;
    public PermanentEffectSO[] zoneEffects => _zoneEffects;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            var levelEffects = _zoneEffects;
            for (int i = 0; i < levelEffects.Length; i++)
            {
                var effect = levelEffects[i];
                player.ApplyEffect(effect);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            var levelEffects = _zoneEffects;
            for (int i = 0; i < levelEffects.Length; i++)
            {
                var effect = levelEffects[i];
                player.RemoveEffect(effect);
            }
        }
    }
}
