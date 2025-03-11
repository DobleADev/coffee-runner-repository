using UnityEngine;
using UnityEngine.UI;

public class LevelFinishedUIManager : MonoBehaviour
{
    [SerializeField] Text _collectedCoinsText;

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _collectedCoinsText.text = GameDataManager.instance.coins.ToString();
    }
}
