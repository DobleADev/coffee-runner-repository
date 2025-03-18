using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopProductsUICollection : UICollection<ShopProductsUIItem>
{
    [SerializeField] List<ShopProductSO> _products;
    [SerializeField] UnityEvent _onPurchase;

    protected override int GetItemsCount()
    {
        return _products.Count;
    }

    protected override void AddItem(ShopProductsUIItem item, int index)
    {
        var product = _products[index];
        item.Setup(product, GameDataManager.instance.coins, () => Purchase(product));
        // item.OnClickEvent += () => Purchase(_products[index]);
    }


    void UpdateProductsState()
    {
        int coins = GameDataManager.instance.coins;
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].UpdateState(_products[i], coins);
        }
    }

    void Purchase(ShopProductSO product)
    {
        try
        {
            int money = GameDataManager.instance.coins;

            if (money >= product.price)
            {
                GameDataManager.instance.coins -= product.price;
                if (product is PowerupsShopProductsSO powerup)
                {
                    GameDataManager.instance.AddOwnedPowerup(powerup.powerup);
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        
        UpdateProductsState();
        _onPurchase?.Invoke();
    }

}

public abstract class ShopProductSO : ScriptableObject
{
    public int price;
    public Sprite thumbnail;
    public abstract string GetName();
}