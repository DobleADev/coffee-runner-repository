using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelController _level;
    [SerializeField] PlayerEffectUIManager _playerEffectUIManager;
    [SerializeField] GameOverUIManager _gameOverUIManager;
    PlayerController _player;
    LevelController _levelInstance;
    [SerializeField] List<PlayerStatusEffectSO> _environmentDefaultEffects = new List<PlayerStatusEffectSO>();
    public List<PlayerStatusEffectSO> GetPermanentEffects => _environmentDefaultEffects;

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

    public void InstanceLevel()
    {
        if (_levelInstance != null)
        {
            Destroy(_levelInstance.gameObject);
        }
        _levelInstance = Instantiate(_level);
    }

    public void InstancePlayer()
    {
        if (_player != null)
        {
            Destroy(_player.gameObject);
        }
        _player = _level.start.Spawn();
        var levelEffects = _environmentDefaultEffects;
        for (int i = 0; i < levelEffects.Count; i++)
        {
            var effect = levelEffects[i];
            if (effect == null) continue;
            _player.ApplyEffect(effect); // Ahora usa PlayerStatusEffectSO
        }
        _playerEffectUIManager.player = _player;
    }

    public void RevivePlayer()
    {
        _player.Revive();
        var levelEffects = _environmentDefaultEffects;
        for (int i = 0; i < levelEffects.Count; i++)
        {
            var effect = levelEffects[i];
            if (effect == null) continue;
            _player.ApplyEffect(effect); // Ahora usa PlayerStatusEffectSO
        }
        _playerEffectUIManager.UpdateEquippedEffectUI();
        _playerEffectUIManager.UpdateUI();
    }

    public void DestroyPlayer()
    {
        Destroy(_player.gameObject);
    }

    public void UpdateGameOverData()
    {
        _gameOverUIManager.playerPosition = _player.transform.position;
        _gameOverUIManager.startPosition = _levelInstance.start.transform.position;
        _gameOverUIManager.endPosition = _levelInstance.end.position;
        _gameOverUIManager.UpdateUI();
    }
}