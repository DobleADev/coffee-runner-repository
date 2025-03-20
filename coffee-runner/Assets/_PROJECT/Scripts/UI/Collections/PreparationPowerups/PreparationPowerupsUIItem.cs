using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPowerupsUIItem : UIItem
{
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _quantityLabel;
    [SerializeField] GameObject _equippedGraphic;
    [SerializeField] Button _equipButton;
    [SerializeField] PlayerStatusEffectOwned powerupOwned;

    public void Setup(PlayerStatusEffectOwned playerStatusEffectOwned)
    {
        powerupOwned = playerStatusEffectOwned;
        _equipButton.onClick.AddListener(() => Equip(!powerupOwned.isEquipped));
        // Equip(powerupOwned.isEquipped);
        Equip(false);
    }

    void Equip(bool equip)
    {
        powerupOwned.isEquipped = equip;
        _nameLabel.text = powerupOwned.statusEffect.effectName + " LV." + powerupOwned.statusEffect.level;
        _quantityLabel.text = (powerupOwned.quantity - (powerupOwned.isEquipped ? 1 : 0)).ToString();
        _equippedGraphic.SetActive(powerupOwned.isEquipped);
    }
}
