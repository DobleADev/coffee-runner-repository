using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] Slider _playerProgress;
    [SerializeField] Text _mettersTravelled;
    [SerializeField] Text _mettersRemaining;
    [SerializeField] Text _collectedCoinsText;
    public Vector3 startPosition { get; set; }
    public Vector3 endPosition { get; set; }
    public Vector3 playerPosition { get; set; }

    public void UpdateUI()
    {
        _playerProgress.minValue = startPosition.x;
        _playerProgress.maxValue = endPosition.x;
        _playerProgress.value = playerPosition.x;

        _mettersTravelled.text = (playerPosition.x - startPosition.x).ToString("N1") + "m";
        _mettersRemaining.text = (endPosition.x - playerPosition.x).ToString("N1") + "m";
        _collectedCoinsText.text = GameDataManager.instance.coins.ToString();
    }
}
