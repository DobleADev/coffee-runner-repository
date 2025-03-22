using UnityEngine;

public class PrefabShopProductsUICollection : ShopProductsUICollection
{
    [SerializeField] private PrefabShopProductsUIDetails _detailsPanel;

    protected override void SetupItem(ShopProductsUIItem item, ShopProductSO product)
    {
        if (item is PrefabShopProductsUIItem prefabItem && product is PrefabShopProductSO prefabProduct)
        {
            prefabItem.Setup(
                prefabProduct,
                GameDataManager.instance.coins,
                () => Purchase(prefabProduct),
                () => ShowDetails(prefabProduct)  // Añadido el delegado para mostrar detalles
            );
        }
    }

    protected override void HandlePurchase(ShopProductSO product)
    {
        if (product is PrefabShopProductSO prefabProduct)
        {
            // Aquí, en lugar de AddOwnedPowerup, necesitas una forma de marcar la skin como comprada.
            // Podrías tener una lista de nombres de prefabs comprados o algo similar en GameDataManager.
            GameDataManager.instance.AddOwnedPrefab(prefabProduct.prefab); // Método hipotético. ¡Asegúrate de implementarlo!
        }
    }


    // Nuevo método ShowDetails para mostrar el panel de detalles
    private void ShowDetails(PrefabShopProductSO product)
    {
        _detailsPanel.Setup(
            product,
            GameDataManager.instance.coins,
            () =>
            {
                Purchase(product);
                _detailsPanel.UpdateState(GameDataManager.instance.coins);  // Asegúrate de actualizar el estado después de comprar
            }
        );
    }
}