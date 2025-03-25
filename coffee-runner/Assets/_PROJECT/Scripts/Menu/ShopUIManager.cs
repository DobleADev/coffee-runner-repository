using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _coinsText;
    [SerializeField] TMP_Text _premiumCoinsText;

    public void UpdateUI()
    {
        _coinsText.text = GameDataManager.instance.coins.ToString();
        _premiumCoinsText.text = GameDataManager.instance.premiumCoins.ToString();
    }
}
