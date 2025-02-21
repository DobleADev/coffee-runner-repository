using UnityEngine;

[CreateAssetMenu(fileName = "FreezeTemperatureEffect", menuName = "Scriptable Objects/Player Effect/Stop Temperature Change Effect")]
public class FreezeTemperatureEffectSO : DurationEffectSO
{
    private float _frozenTemperature;
    private float _previousEffectTemperature;

    // public override void Apply(PlayerController player)
    // {
    //     _frozenTemperature = player.Temperature;
    //     _previousEffectTemperature = player.EffectTemperature;
    //      // Desactiva cambios de temperatura base
    // }

    public override void Update(PlayerController player)
    {
        // Mantiene la temperatura congelada
        player.CapTemperature();
    }

    // public override void Remove(PlayerController player)
    // {
    //     player.EffectTemperature = _previousEffectTemperature; // Restaura los cambios de temperatura base
    // }
}