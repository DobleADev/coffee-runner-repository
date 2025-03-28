using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerPrefab;
    [SerializeField] GameObject _defaultSkinPrefab;
    [SerializeField] LevelReferenceSO _currentLevel;
    [SerializeField] PlayerEffectUIManager _playerEffectUIManager;
    [SerializeField] GameOverUIManager _gameOverUIManager;
    [SerializeField] LevelFinishedUIManager _levelFinishedUIManager;
    [SerializeField] UnityEvent _onErrorLevelLoading;
    [SerializeField] UnityEvent _onSuccessfullLevelLoading;
    PlayerController _player;
    LevelController _levelInstance;
    private int _coinsCollected = 0;
    public void AddCoin() { _coinsCollected ++; }
    public void ResetCoins() { _coinsCollected = 0; }

    public void InstanceLevel()
    {
        if (_currentLevel == null)
        {
            return;
        }

        if (_levelInstance != null)
        {
            Destroy(_levelInstance.gameObject);
        }

        if (_currentLevel.level.prefab == null)
        {
            _onErrorLevelLoading?.Invoke();
            return;
        }
        
        _levelInstance = Instantiate(_currentLevel.level.prefab);
        ResetCoins();
        _onSuccessfullLevelLoading?.Invoke();
    }

    public void InstancePlayer()
    {
        if (_levelInstance == null)
        {
            return;
        }

        //Destroy old player
        if (_player != null)
        {
            Destroy(_player.gameObject);
        }

        //INSTANTIATE PLAYER HERE :')
        _player = Instantiate(_playerPrefab);
        var skinPrefab = GameDataManager.instance.currentPlayerSkin;
        if (skinPrefab != null)
        {
            Instantiate(skinPrefab, _player.transform);
        }
        else
        {
            Instantiate(_defaultSkinPrefab, _player.transform);
        }
        _levelInstance.start.Spawn(_player.gameObject);


        var levelEffects = _currentLevel.level.prefab.EnvironmentDefaultEffects;
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
        var levelEffects = _currentLevel.level.prefab.EnvironmentDefaultEffects;
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

    public void CompleteLevel()
    {
        _levelFinishedUIManager.coinsCollected = _coinsCollected;
        GameDataManager.instance.CompleteLevel(_currentLevel.level.levelName);
    }

    public void UpdateGameOverData()
    {
        _gameOverUIManager.coinsCollected = _coinsCollected;
        _gameOverUIManager.playerPosition = _player.transform.position;
        _gameOverUIManager.startPosition = _levelInstance.start.transform.position;
        _gameOverUIManager.endPosition = _levelInstance.end.position;
        _gameOverUIManager.UpdateUI();
    }
}