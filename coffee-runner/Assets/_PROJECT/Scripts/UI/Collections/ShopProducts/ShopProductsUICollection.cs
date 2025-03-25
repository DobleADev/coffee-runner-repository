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
        int premiumCoins = GameDataManager.instance.premiumCoins;
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].UpdateState(_products[i], coins, premiumCoins);
        }
    }

    protected void Purchase(ShopProductSO product)
    {
        try
        {
            if (product.price == 0)
            {
                HandlePurchase(product);
            }
            else
            {
                switch (product.currencyType)
                {
                    case CurrencyType.Coins:
                        if (GameDataManager.instance.coins >= product.price)
                        {
                            GameDataManager.instance.coins -= product.price;
                            HandlePurchase(product);
                        }
                        break;
                    case CurrencyType.PremiumCoins:
                        if (GameDataManager.instance.premiumCoins >= product.price)
                        {
                            GameDataManager.instance.premiumCoins -= product.price;
                            HandlePurchase(product);
                        }
                        break;
                    case CurrencyType.RealMoney:
                        // Lógica para compras con dinero real (e.g., IAP)
                        HandleRealMoneyPurchase(product);
                        break;
                    default:
                        Debug.LogError("Tipo de moneda no soportado.");
                        break;
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
    protected virtual void HandleRealMoneyPurchase(ShopProductSO product)
    {
        // Implementación predeterminada para compras con dinero real (puedes sobreescribirla en clases derivadas)
        Debug.LogWarning("Compra con dinero real no implementada.");
    }
    protected abstract void HandlePurchase(ShopProductSO product);
}

public abstract class ShopProductSO : ScriptableObject
{
    public int price;
    public CurrencyType currencyType;
    public Sprite thumbnail;
    public abstract string GetName();
}

public enum CurrencyType
{
    Coins,
    PremiumCoins,
    RealMoney
}