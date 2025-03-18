using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupsUIItem : MonoBehaviour
{
    public TMP_Text label;
    public Image equippedImage;
    public PlayerStatusEffectSO powerUp;
    public event Action<PlayerStatusEffectSO> OnPowerUpSelected;

    public void SetUp(PlayerStatusEffectSO powerUp)
    {
        this.powerUp = powerUp;
        label.text = powerUp.effectName;
        // GetComponent<Image>().sprite = powerUp.icon; // Display the power-up icon
    }

    public void OnClick()
    {
        OnPowerUpSelected?.Invoke(powerUp);
    }

    public void SetEquippedStatus(bool isEquipped)
    {
        equippedImage.gameObject.SetActive(isEquipped);
    }
}
