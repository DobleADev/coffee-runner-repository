using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupsUIEquippedItem : MonoBehaviour
{
    public TMP_Text label;
    public PlayerEffectSO powerUp;
    public PlayerPowerupsUIManager powerUpWindow; // Reference to the main window script

    public void SetUp(PlayerEffectSO powerUp, PlayerPowerupsUIManager powerUpWindow)
    {
        this.powerUp = powerUp;
        this.powerUpWindow = powerUpWindow;
        label.text = powerUp.effectName;
        // GetComponent<Image>().sprite = powerUp.icon; // Display the power-up icon
    }

    public void OnClick()
    {
        powerUpWindow.DequipPowerUp(powerUp);
    }
}
