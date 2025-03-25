using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPowerupsUIDetails : MonoBehaviour
{
    [SerializeField] private Image _demonstrationImage;
    [SerializeField] private TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _upgradeDetailsText;
    [SerializeField] private GameObject _panel;

    public void Setup(PlayerStatusEffectOwned ownedPowerup)
    {
        var powerup = ownedPowerup.statusEffect;
        _nameLabel.text = powerup.effectName;
        _quantityText.text = "You have: " + ownedPowerup.quantity.ToString();
        _descriptionText.text = powerup.description;
        _upgradeDetailsText.text = powerup.GetUpgradeDescription();
        // _demonstrationImage.sprite = powerup.thumbnail;
        _panel.SetActive(true);
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}