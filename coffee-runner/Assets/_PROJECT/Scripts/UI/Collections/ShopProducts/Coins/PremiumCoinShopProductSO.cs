using UnityEngine;

[CreateAssetMenu(fileName = "PremiumCoinShopProduct", menuName = "Scriptable Objects/Shop Product/Premium Coin")]
public class PremiumCoinShopProductSO : ShopProductSO
{
    public int premiumCoins;
    public override string GetName()
    {
        return premiumCoins.ToString() + " Premium Coins";
    }
}
