using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinShopUI : MonoBehaviour
{
    [SerializeField] TMP_Text _skinNameLabel;
    [SerializeField] Image _skinPreview;
    [SerializeField] Button _buyEquipButton;  // Renombrado para reflejar doble funcionalidad
    [SerializeField] TMP_Text _buyEquipButtonText; // Texto del botón (Comprar/Equipar)
    [SerializeField] UnityEvent _onPurchase;
    // [SerializeField] TMP_Text _playerCoinsText;    // Para mostrar las monedas del jugador

    [SerializeField] List<PrefabShopProductSO> _shopSkins = new List<PrefabShopProductSO>(); // Lista de PRODUCTOS, no prefabs directos
    int _currentIndex = 0;

    void Start()
    {
        UpdateShopList();        
        // UpdatePlayerCoinsText(); // Inicializa el texto de monedas
    }


    void ShowSelectedSkin()
    {
        _skinNameLabel.text = _shopSkins[_currentIndex].GetName();  // Usamos GetName() del SO

        // Mostrar el sprite.  Asume que el prefab tiene un SpriteRenderer en la raíz.
        // Si no,  necesitarás ajustar esto.
        var skinSprite = _shopSkins[_currentIndex].prefab.GetComponent<SpriteRenderer>();
        _skinPreview.sprite = skinSprite.sprite;
        _skinPreview.color = skinSprite.color;

        UpdateBuyEquipButton();
    }

    void UpdateBuyEquipButton()
    {
        bool isOwned = GameDataManager.instance.ownedSkins.Contains(_shopSkins[_currentIndex].prefab);
        bool isEquipped = GameDataManager.instance.currentPlayerSkin == _shopSkins[_currentIndex].prefab;


        // if (isEquipped)
        // {
        //     _buyEquipButtonText.text = "Equipado";
        //     _buyEquipButton.interactable = false;
        // }
        // else if (isOwned)
        // {
        //     _buyEquipButtonText.text = "Equipar";
        //     _buyEquipButton.interactable = true;
        // }
        if (isOwned)
        {
            _buyEquipButtonText.text = "Owned";
            _buyEquipButton.interactable = false;
        }
        else
        {
            // No poseído:  Mostrar precio.
            _buyEquipButtonText.text = $"Buy ({_shopSkins[_currentIndex].price})";
            _buyEquipButton.interactable = GameDataManager.instance.coins >= _shopSkins[_currentIndex].price;
        }
    }

    // Carga los productos desde el ShopSO
    public void UpdateShopList()
    {
        // _shopSkins.Clear();
        // _shopSkins.AddRange(_shopData.products); // Usamos la lista directamente del ShopSO
        _currentIndex = 0;
        if (_shopSkins.Count > 0) // Evitamos errores si la tienda está vacía
        {
            ShowSelectedSkin();
        }
        else
        {
            // Manejo del caso de tienda vacía (opcional)
            _skinNameLabel.text = "No hay skins disponibles";
            _skinPreview.sprite = null; // Limpia la preview
            _buyEquipButton.interactable = false;
        }
    }


    public void BuyEquipSelected()
    {
        bool isOwned = GameDataManager.instance.ownedSkins.Contains(_shopSkins[_currentIndex].prefab);

        if (isOwned)
        {
            // Equipar
            GameDataManager.instance.currentPlayerSkin = _shopSkins[_currentIndex].prefab;
        }
        else
        {
            // Intentar comprar
            if (GameDataManager.instance.coins >= _shopSkins[_currentIndex].price)
            {
                GameDataManager.instance.coins -= _shopSkins[_currentIndex].price;
                GameDataManager.instance.ownedSkins.Add(_shopSkins[_currentIndex].prefab);
                // UpdatePlayerCoinsText();  // Actualizar monedas
                _onPurchase?.Invoke();
            }
            else
            {
                // Podrías mostrar un mensaje "Monedas insuficientes" aquí
                Debug.Log("Monedas Insuficientes!"); // O un mensaje en la UI
                return; // Importante: Salir para no actualizar el botón si la compra falla.
            }
        }

        UpdateBuyEquipButton(); // Actualiza *después* de comprar/equipar.
    }


    public void NextSkin()
    {
        if (_shopSkins.Count == 0) return; // Evitar errores si la tienda está vacía
        _currentIndex += _currentIndex < _shopSkins.Count - 1 ? 1 : -(_shopSkins.Count - 1);
        ShowSelectedSkin();
    }

    public void PreviousSkin()
    {
        if (_shopSkins.Count == 0) return; // Evitar errores si la tienda está vacía
        _currentIndex += _currentIndex > 0 ? -1 : _shopSkins.Count - 1;
        ShowSelectedSkin();
    }

    // // Actualiza el texto que muestra las monedas del jugador
    // void UpdatePlayerCoinsText()
    // {
    //     if (_playerCoinsText != null) // Comprobación para evitar errores si no se asigna
    //     {
    //         _playerCoinsText.text = $"Monedas: {GameDataManager.instance.coins}";
    //     }
    // }

    //Opcional, para llamar desde otro script, por ejemplo cuando se ganan monedas.
    public void RefreshUI()
    {
        // UpdatePlayerCoinsText();
        UpdateBuyEquipButton();
    }
}