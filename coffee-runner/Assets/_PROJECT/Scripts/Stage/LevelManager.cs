using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelDataSO _level;
    [SerializeField] PlayerEffectUIManager _playerEffectUIManager;
    [SerializeField] GameOverUIManager _gameOverUIManager;
    PlayerController _player;
    LevelController _levelInstance;

    public void InstanceLevel()
    {
        if (_level == null)
        {
            return;
        }

        if (_levelInstance != null)
        {
            Destroy(_levelInstance.gameObject);
        }
        _levelInstance = Instantiate(_level.prefab);
    }

    public void InstancePlayer()
    {
        if (_levelInstance == null)
        {
            return;
        }

        if (_player != null)
        {
            Destroy(_player.gameObject);
        }
        _player = _levelInstance.start.Spawn();
        var levelEffects = _level.prefab.EnvironmentDefaultEffects;
        for (int i = 0; i < levelEffects.Count; i++)
        {
            var effect = levelEffects[i];
            if (effect == null) continue;
            _player.ApplyEffect(effect); // Ahora usa PlayerStatusEffectSO
        }
        _playerEffectUIManager.player = _player;
        _playerEffectUIManager.level = _levelInstance;
    }

    public void RevivePlayer()
    {
        _player.Revive();
        var levelEffects = _level.prefab.EnvironmentDefaultEffects;
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