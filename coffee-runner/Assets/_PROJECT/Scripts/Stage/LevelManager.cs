using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PermanentEffectSO[] _environmentDefaultEffects;
    public PermanentEffectSO[] GetPermanentEffects => _environmentDefaultEffects;
}
