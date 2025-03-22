using UnityEngine;

[CreateAssetMenu(fileName = "SkinShopProduct", menuName = "Scriptable Objects/Shop Product/Skin")]
public class PrefabShopProductSO : ShopProductSO
{
    public GameObject prefab;

    public override string GetName()
    {
        return prefab.name;
    }
}
