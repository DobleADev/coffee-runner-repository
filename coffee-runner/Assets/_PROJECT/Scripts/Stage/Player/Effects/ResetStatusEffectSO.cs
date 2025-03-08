using UnityEngine;

[CreateAssetMenu(fileName = "NewResetEffectsProperty", menuName = "Scriptable Objects/Effect Properties/Reset Effects")]
public class ResetStatusEffectSO : StatusEffectProperty
{
    public override void Apply(PlayerController player)
    {
        for (int i = player.activeEffects.Count - 1; i >= 0 ; i--)
        {
            var effect = player.activeEffects[i];
            player.RemoveEffect(effect.effect);
        }
    }
}
