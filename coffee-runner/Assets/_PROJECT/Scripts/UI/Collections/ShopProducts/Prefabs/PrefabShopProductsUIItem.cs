using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrefabShopProductsUIItem : ShopProductsUIItem
{
   [SerializeField] Button _infoButton; // Botón de información

    // No necesitas _upgradePriceLabel ni _upgradeButton porque las skins no se mejoran.

    public void Setup(PrefabShopProductSO product, int money, UnityAction buyCall, UnityAction infoCall) // Añadido el parámetro infoCall
    {
        _infoButton.onClick.AddListener(infoCall); // Añadir listener al botón de información
        Setup(product, money, buyCall);  // Llama al Setup base para configurar el botón de compra.
    }

    public override void Setup(ShopProductSO product, int money, UnityAction buyCall)
    {
        UpdateState(product, money); // Luego actualizamos
        _buyButton.onClick.AddListener(buyCall);
    }

    public override void UpdateState(ShopProductSO product, int money)
    {
        _nameLabel.text = product.GetName();
        _priceLabel.text = "Buy $" + product.price.ToString();
        if (product.thumbnail != null) _thumbnailImage.sprite = product.thumbnail;
        _buyButton.interactable = money >= product.price;

        // No hay lógica de upgrade aquí, así que lo quitamos.
    }
}