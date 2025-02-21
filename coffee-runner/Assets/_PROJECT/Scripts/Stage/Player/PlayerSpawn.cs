using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] PlayerEffectUIManager _playerEffectUIManager;
    [SerializeField] PlayerController _playerPrefab;
    
    public void Spawn()
    {
        var newPlayer = Instantiate(_playerPrefab);
        newPlayer.transform.position = transform.position;
        var levelEffects = _levelManager.GetPermanentEffects;
        for (int i = 0; i < levelEffects.Length; i++)
        {
            var effect = levelEffects[i];
            newPlayer.ApplyEffect(effect);
        }

        _playerEffectUIManager.player = newPlayer;
    }
}
