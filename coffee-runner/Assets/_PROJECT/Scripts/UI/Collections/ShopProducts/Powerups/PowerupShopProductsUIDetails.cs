using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerupShopProductsUIDetails : MonoBehaviour
{
    [SerializeField] private Image _demonstrationImage; // O VideoPlayer
    [SerializeField] protected TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _upgradeDetailsText;
    [SerializeField] protected TMP_Text _priceLabel;
    [SerializeField] TMP_Text _upgradePriceLabel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject _panel;

    private PowerupsShopProductsSO _currentProduct;

    public void Setup(PowerupsShopProductsSO product, int money, UnityAction buyCall, UnityAction upgradeCall)
    {
        _currentProduct = product;
        _descriptionText.text = product.powerup.description;
        _priceLabel.text = "Buy $" + product.price.ToString();
        _demonstrationImage.sprite = product.thumbnail; // Cambiar según el tipo de demostración
        _buyButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(buyCall);
        _upgradeButton.onClick.AddListener(upgradeCall);
        UpdateState(money);
        _panel.SetActive(true);
    }

    public void UpdateState(int money)
    {
        if (_currentProduct == null) return;

        bool maxLevel = _currentProduct.powerup.level >= _currentProduct.powerup.maxLevel;
        _nameLabel.text = _currentProduct.GetName() + " Lv." + _currentProduct.powerup.level;
        _upgradeDetailsText.text = _currentProduct.powerup.GetUpgradeDescription();
        _buyButton.interactable = money >= _currentProduct.price;
        _upgradeButton.interactable = money >= _currentProduct.upgradePrice.Value(_currentProduct.powerup.level) && !maxLevel;
        _buyButton.gameObject.SetActive(!_currentProduct.onlyUpgreadable);

        _upgradePriceLabel.text = maxLevel ? "MAX LEVEL"
            : "Upgrade to Lv." + (_currentProduct.powerup.level + 1) + " $" + _currentProduct.upgradePrice.Value(_currentProduct.powerup.level).ToString();
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}
