using UnityEngine;

public class CoinShopProductsUICollection : ShopProductsUICollection
{
    protected override void HandlePurchase(ShopProductSO product)
    {
        switch (product)
        {
            case CoinShopProductSO coin:
            {
                GameDataManager.instance.coins += coin.coins;
            } break;

            case PremiumCoinShopProductSO premiumCoin:
            {
                GameDataManager.instance.premiumCoins += premiumCoin.premiumCoins;
            } break;

            // default:
            // {
                
            // } break;
        }
    }

    protected override void SetupItem(ShopProductsUIItem item, ShopProductSO product)
    {
        switch (product)
        {
            case CoinShopProductSO coin:
            {
                
            } break;

            case PremiumCoinShopProductSO premiumCoin:
            {
                
            } break;

            // default:
            // {
                
            // } break;
        }

        item.Setup(
                product,
                GameDataManager.instance.coins,
                () => Purchase(product)
            );
    }
}
