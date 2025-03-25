using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryPowerupsUIItem : UIItem
{
    [SerializeField] private Image _thumbnailImage;
    [SerializeField] private TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _quantityLabel;
    [SerializeField] private Button _itemButton;

    public void Setup(PlayerStatusEffectOwned ownedPowerup, UnityAction onClick)
    {
        var powerup = ownedPowerup.statusEffect;
        _nameLabel.text = powerup.effectName;
        _quantityLabel.text = ownedPowerup.quantity.ToString();
        // _thumbnailImage.sprite = powerup;
        _itemButton.onClick.AddListener(onClick);
    }
}