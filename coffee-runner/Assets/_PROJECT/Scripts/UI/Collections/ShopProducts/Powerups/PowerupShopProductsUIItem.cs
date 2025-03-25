using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PowerupShopProductsUIItem : ShopProductsUIItem
{
    [SerializeField] TMP_Text _upgradePriceLabel;
    [SerializeField] Button _upgradeButton;
    [SerializeField] Button _infoButton; // Botón de información
    private PowerupsShopProductsSO _powerupProduct;

    public void Setup(PowerupsShopProductsSO product, int money, int premiumMoney, UnityAction buyCall, UnityAction upgradeCall, UnityAction infoCall)
    {
        _powerupProduct = product;
        _upgradeButton.onClick.AddListener(upgradeCall);
        _infoButton.onClick.AddListener(infoCall); // Añadir listener al botón de información
        UpdateState(product, money, premiumMoney);
        _buyButton.onClick.AddListener(buyCall);
    }

    public override void Setup(ShopProductSO product, int money, UnityAction buyCall)
    {
        // UpdateState(product, money, premiumMoney);
        _buyButton.onClick.AddListener(buyCall);
    }

    public override void UpdateState(ShopProductSO product, int money, int premiumMoney)
    {
        _nameLabel.text = product.GetName();
        _priceLabel.text = "Buy $" + product.price.ToString();
        if (product.thumbnail != null) _thumbnailImage.sprite = product.thumbnail;
        _buyButton.interactable = money >= product.price;

        bool maxLevel = _powerupProduct.powerup.level >= _powerupProduct.powerup.maxLevel;
        _buyButton.gameObject.SetActive(!_powerupProduct.onlyUpgreadable);

        _upgradePriceLabel.text = maxLevel ? "MAX LEVEL"
            : "Upgrade to Lv." + (_powerupProduct.powerup.level + 1) + " $" + _powerupProduct.upgradePrice.Value(_powerupProduct.powerup.level).ToString();
        _upgradeButton.interactable = money >= _powerupProduct.upgradePrice.Value(_powerupProduct.powerup.level) && !maxLevel;
    }
}