using UnityEngine;
using UnityEngine.Events;

public class CoinShopProductsUIItem : ShopProductsUIItem
{
    public override void Setup(ShopProductSO product, int money, UnityAction buyCall)
    {
        _buyButton.onClick.AddListener(buyCall);
        UpdateState(product, money);
    }

    public override void UpdateState(ShopProductSO product, int money, int premiumMoney = 0)
    {
        _nameLabel.text = product.GetName();
        switch (product)
        {
            case CoinShopProductSO coin:
            {
                _priceLabel.text = "Buy $" + product.price.ToString() + " premiums";
            } break;

            case PremiumCoinShopProductSO premiumCoin:
            {
                if (product.price > 0)
                {
                    _priceLabel.text = "Buy USD$" + product.price.ToString();
                }
                else
                {
                    _priceLabel.text = "FREE";
                }
            } break;

            // default:
            // {
                
            // } break;
        }

        // _priceLabel.text = "Buy $" + product.price.ToString();
        if (product.thumbnail != null) _thumbnailImage.sprite = product.thumbnail;
        // _buyButton.interactable = money >= product.price;

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
    }
}
