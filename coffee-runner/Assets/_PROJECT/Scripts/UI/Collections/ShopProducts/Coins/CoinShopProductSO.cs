using UnityEngine;

[CreateAssetMenu(fileName = "CoinShopProduct", menuName = "Scriptable Objects/Shop Product/Coin")]
public class CoinShopProductSO : ShopProductSO
{
    public int coins;
    public override string GetName()
    {
        return coins.ToString() + " Coins";
    }
}
