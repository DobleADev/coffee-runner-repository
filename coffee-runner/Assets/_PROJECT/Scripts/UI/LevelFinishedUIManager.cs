using UnityEngine;
using UnityEngine.UI;

public class LevelFinishedUIManager : MonoBehaviour
{
    [SerializeField] Text _collectedCoinsText;
    public int coinsCollected { get; set; }

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _collectedCoinsText.text = coinsCollected.ToString();
    }
}
