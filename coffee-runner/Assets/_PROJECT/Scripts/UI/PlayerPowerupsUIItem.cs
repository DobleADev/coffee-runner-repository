using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupsUIItem : MonoBehaviour
{
    public TMP_Text label;
    public Image equippedImage;
    public PlayerStatusEffectSO powerUp;
    public PlayerPowerupsUIManager powerUpWindow; // Reference to the main window script

    public void SetUp(PlayerStatusEffectSO powerUp, PlayerPowerupsUIManager powerUpWindow)
    {
        this.powerUp = powerUp;
        this.powerUpWindow = powerUpWindow;
        label.text = powerUp.effectName;
        // GetComponent<Image>().sprite = powerUp.icon; // Display the power-up icon
    }

    public void OnClick()
    {
        powerUpWindow.ShowPowerUpDetails(powerUp);
    }

    public void SetEquippedStatus(bool isEquipped)
    {
        equippedImage.gameObject.SetActive(isEquipped);
    }
}
