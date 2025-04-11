using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PreparationPowerupShopProductsUIItem : ShopProductsUIItem
{
    private PowerupsShopProductsSO _powerupProduct;
    [SerializeField] GameObject _equippedGraphic;

    public void Setup(PowerupsShopProductsSO product, int money, int premiumMoney, UnityAction actionCall)
    {
        _powerupProduct = product;
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(actionCall);
        UpdateState(product, money, premiumMoney);
    }

    public override void Setup(ShopProductSO product, int money, UnityAction buyCall)
    {
        // throw new System.NotImplementedException();
    }

    public override void UpdateState(ShopProductSO product, int money, int premiumMoney)
    {
        var ownedPowerup = GameDataManager.instance.GetOwnedPowerup(_powerupProduct.powerup);
        _nameLabel.text = product.GetName();
        _priceLabel.text = ownedPowerup != null ? (ownedPowerup.quantity - (ownedPowerup.isEquipped ? 1 : 0)).ToString()
        // _priceLabel.text = ownedPowerup != null ? (ownedPowerup.isEquipped ? "Equipped" : "Equip").ToString()
        : "Buy $" + product.price.ToString();
        if (product.thumbnail != null) _thumbnailImage.sprite = product.thumbnail;
        // _buyButton.interactable = money >= product.price || ownedPowerup != null;

        switch (product.currencyType)
        {
            case CurrencyType.Coins:
                _buyButton.interactable = money >= product.price;
                break;
            case CurrencyType.PremiumCoins:
                _buyButton.interactable = premiumMoney >= product.price;
                break;
            case CurrencyType.RealMoney:
                _buyButton.interactable = true; // Siempre interactuable para compras con dinero real
                break;
        }

        // _buyButton.GetComponentInChildren<TMP_Text>().text = ownedPowerup != null? "Equip" : "Buy";

        if (ownedPowerup == null) 
        {
            _equippedGraphic.SetActive(false);
            return;
        }
        
        _equippedGraphic.SetActive(ownedPowerup.isEquipped);
        // Debug.Log("UPDATED");
    }
}