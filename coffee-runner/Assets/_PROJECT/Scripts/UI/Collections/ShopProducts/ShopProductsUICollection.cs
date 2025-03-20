using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ShopProductsUICollection : UICollection<ShopProductsUIItem>
{
    [SerializeField] protected List<ShopProductSO> _products;
    [SerializeField] protected UnityEvent _onPurchase;

    protected override int GetItemsCount()
    {
        return _products.Count;
    }

    protected override void AddItem(ShopProductsUIItem item, int index)
    {
        SetupItem(item, _products[index]);
    }

    protected abstract void SetupItem(ShopProductsUIItem item, ShopProductSO product);

    protected void UpdateProductsState()
    {
        int coins = GameDataManager.instance.coins;
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].UpdateState(_products[i], coins);
        }
    }

    protected void Purchase(ShopProductSO product)
    {
        try
        {
            int money = GameDataManager.instance.coins;

            if (money >= product.price)
            {
                GameDataManager.instance.coins -= product.price;
                HandlePurchase(product);
            }
        }
        catch (System.Exception)
        {
            throw;
        }

        UpdateProductsState();
        _onPurchase?.Invoke();
    }

    protected abstract void HandlePurchase(ShopProductSO product);
}

public abstract class ShopProductSO : ScriptableObject
{
    public int price;
    public Sprite thumbnail;
    public abstract string GetName();
}