using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrefabShopProductsUIDetails : MonoBehaviour
{
    [SerializeField] private Image _demonstrationImage; // Puedes usar esto para mostrar una captura de pantalla del prefab.
    [SerializeField] protected TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _descriptionText; // Si tienes descripciones para las skins.
    [SerializeField] protected TMP_Text _priceLabel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _panel;

    private PrefabShopProductSO _currentProduct;

    public void Setup(PrefabShopProductSO product, int money, UnityAction buyCall)
    {
        _currentProduct = product;
        _nameLabel.text = product.GetName();
        // _descriptionText.text = product.description;  // Asegúrate de que ShopProductSO tenga un campo 'description'.
        _priceLabel.text = "Buy $" + product.price.ToString();
        _demonstrationImage.sprite = product.thumbnail; // Asume que tienes una miniatura para la skin.
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(buyCall);
        UpdateState(money);
        _panel.SetActive(true);
    }
    
     public void Setup(PrefabShopProductSO product, int money, UnityAction buyCall, UnityAction upgradeCall)
    {
       Setup(product, money, buyCall);
    }

    public void UpdateState(int money)
    {
        if (_currentProduct == null) return;

        _nameLabel.text = _currentProduct.GetName();
        _buyButton.interactable = money >= _currentProduct.price;
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}