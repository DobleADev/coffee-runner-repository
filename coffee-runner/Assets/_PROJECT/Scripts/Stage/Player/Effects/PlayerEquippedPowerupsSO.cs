using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEquippedPowerups", menuName = "Scriptable Objects/Player Equipped Powerups")]
public class PlayerEquippedPowerups : ScriptableObject
{
    public List<PlayerEffectSO> powerups;
}
