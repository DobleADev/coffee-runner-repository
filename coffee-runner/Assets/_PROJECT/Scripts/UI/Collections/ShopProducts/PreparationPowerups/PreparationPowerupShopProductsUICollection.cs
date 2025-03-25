using UnityEngine;

public class PreparationPowerupShopProductsUICollection : ShopProductsUICollection
{
    [SerializeField] PlayerStatusEffectContainerSO _equippedPowerups;

    protected override void SetupItem(ShopProductsUIItem item, ShopProductSO product)
    {
        if (item is PreparationPowerupShopProductsUIItem preparationItem && product is PowerupsShopProductsSO powerupProduct)
        {
            preparationItem.Setup(
                powerupProduct,
                GameDataManager.instance.coins,
                () => HandleAction(powerupProduct)
            );
        }
    }

    private void HandleAction(PowerupsShopProductsSO powerupProduct)
    {
        if (GameDataManager.instance.GetOwnedPowerup(powerupProduct.powerup) != null)
        {
            ToggleEquip(powerupProduct.powerup);
        }
        else
        {
            Purchase(powerupProduct);
        }
    }

    protected override void HandlePurchase(ShopProductSO product)
    {
        if (product is PowerupsShopProductsSO powerup)
        {
            GameDataManager.instance.AddOwnedPowerup(powerup.powerup);
        }
    }

    private void ToggleEquip(PlayerStatusEffectSO powerup)
    {
        var ownedPowerup = GameDataManager.instance.GetOwnedPowerup(powerup);
        ownedPowerup.isEquipped = !ownedPowerup.isEquipped;
        // GameDataManager.instance.ToggleEquipOwnedPowerup(powerup);
        UpdateProductsState();
        // UpdateEquippedPowerups();
    }

    public void ApplyEquippedPowerups()
    {
        _equippedPowerups.powerups = GameDataManager.instance.EquipOwnedPowerups();
    }
}