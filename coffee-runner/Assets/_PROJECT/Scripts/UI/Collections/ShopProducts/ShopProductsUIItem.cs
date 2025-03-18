using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopProductsUIItem : UIItem
{
    // [SerializeField] ShopProductSO _product;
    // ShopProductsUICollection _collection;
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _priceLabel;
    [SerializeField] Image _thumbnailImage;
    [SerializeField] Button _buyButton;

    public void Setup(ShopProductSO product, int money, UnityEngine.Events.UnityAction call)
    {
        UpdateState(product, money);
        _buyButton.onClick.AddListener(call);
    }

    public void UpdateState(ShopProductSO product, int money)
    {
        // Debug.Log(name + " - " + product.GetName());
        _nameLabel.text = product.GetName();
        _priceLabel.text = product.price.ToString();
        if (product.thumbnail != null) _thumbnailImage.sprite = product.thumbnail;
        _buyButton.interactable = money >= product.price;
    }

}
