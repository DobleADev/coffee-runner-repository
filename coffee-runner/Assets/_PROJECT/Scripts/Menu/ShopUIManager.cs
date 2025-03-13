using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _coinsText;

    public void UpdateUI()
    {
        _coinsText.text = GameDataManager.instance.coins.ToString();
    }
}
