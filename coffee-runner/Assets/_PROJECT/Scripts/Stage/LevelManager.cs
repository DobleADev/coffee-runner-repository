using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelController _level;
    [SerializeField] PlayerEffectUIManager _playerEffectUIManager;
    [SerializeField] GameOverUIManager _gameOverUIManager;
    PlayerController _player;
    LevelController _levelInstance;
    [SerializeField] PermanentEffectSO[] _environmentDefaultEffects;
    public PermanentEffectSO[] GetPermanentEffects => _environmentDefaultEffects;

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
        var levelEffects = GetPermanentEffects;
        for (int i = 0; i < levelEffects.Length; i++)
        {
            var effect = levelEffects[i];
            _player.ApplyEffect(effect);
        }
        _playerEffectUIManager.player = _player;
    }

    public void RevivePlayer()
    {
        // _player.gameObject.SetActive(true);
        // _player.EffectTemperature = 0;
        _player.Revive();
        var levelEffects = GetPermanentEffects;
        for (int i = 0; i < levelEffects.Length; i++)
        {
            var effect = levelEffects[i];
            _player.ApplyEffect(effect);
        }
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
