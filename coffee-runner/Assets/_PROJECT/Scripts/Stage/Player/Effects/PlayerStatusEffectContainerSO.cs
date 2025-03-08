using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEffectContainer", menuName = "Scriptable Objects/Player Effect Container")]
public class PlayerStatusEffectContainerSO : ScriptableObject
{
    public List<PlayerStatusEffectSO> powerups;
}
