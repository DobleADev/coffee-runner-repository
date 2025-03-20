using UnityEngine;

public class PowerupShopProductsUICollection : ShopProductsUICollection
{
    [SerializeField] private PowerupShopProductsUIDetails _detailsPanel;

    protected override void SetupItem(ShopProductsUIItem item, ShopProductSO product)
    {
        if (item is PowerupShopProductsUIItem powerupItem && product is PowerupsShopProductsSO powerupProduct)
        {
            powerupItem.Setup(
                powerupProduct,
                GameDataManager.instance.coins,
                () => Purchase(powerupProduct),
                () => Upgrade(powerupProduct),
                () => ShowDetails(powerupProduct)
            );
        }
    }

    protected override void HandlePurchase(ShopProductSO product)
    {
        if (product is PowerupsShopProductsSO powerup)
        {
            GameDataManager.instance.AddOwnedPowerup(powerup.powerup);
        }
    }

    private void Upgrade(PowerupsShopProductsSO product)
    {
        // Debug.Log("WTF");
        try
        {
            int money = GameDataManager.instance.coins;

            if (money >= product.upgradePrice.Value(product.powerup.level) && product.powerup.level < product.powerup.maxLevel)
            {
                GameDataManager.instance.coins -= product.upgradePrice.Value(product.powerup.level);
                product.powerup.level++;
            }
        }
        catch (System.Exception)
        {
            throw;
        }

        UpdateProductsState();
        _onPurchase?.Invoke();
    }

    private void ShowDetails(PowerupsShopProductsSO product)
    {
        _detailsPanel.Setup(
            product,
            GameDataManager.instance.coins,
            () =>
            {
                Purchase(product);
                _detailsPanel.UpdateState(GameDataManager.instance.coins);
            },
            () =>
            {
                Upgrade(product);
                _detailsPanel.UpdateState(GameDataManager.instance.coins);
            }
        );
    }
}