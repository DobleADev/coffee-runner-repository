using UnityEngine;

[CreateAssetMenu(fileName = "PowerupsShopProduct", menuName = "Scriptable Objects/Shop Product/Powerup")]
public class PowerupsShopProductsSO : ShopProductSO
{
    public PlayerStatusEffectSO powerup;
    public override string GetName()
    {
        return powerup.effectName;
    }
}
