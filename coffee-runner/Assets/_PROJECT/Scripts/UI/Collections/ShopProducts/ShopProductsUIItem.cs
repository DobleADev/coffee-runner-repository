using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class ShopProductsUIItem : UIItem
{
    [SerializeField] protected TMP_Text _nameLabel;
    [SerializeField] protected TMP_Text _priceLabel;
    [SerializeField] protected Image _thumbnailImage;
    [SerializeField] protected Button _buyButton;

    public abstract void Setup(ShopProductSO product, int money, UnityAction buyCall);
    public abstract void UpdateState(ShopProductSO product, int money = 0, int premiumMoney = 0);
}