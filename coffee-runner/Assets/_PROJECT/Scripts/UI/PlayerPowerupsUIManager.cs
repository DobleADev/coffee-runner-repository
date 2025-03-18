using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupsUIManager : MonoBehaviour
{
    public PlayerStatusEffectContainerSO availablePowerUps; // List of all equippable power-ups
    public PlayerStatusEffectContainerSO powerups; // Scriptable object for equipped power-ups

    public GameObject powerUpButtonPrefab; // Prefab for equippable power-up buttons
    public Transform powerUpListContent; // Parent transform for the equippable power-up list

    public GameObject equippedPowerUpButtonPrefab; // Prefab for equipped power-up buttons
    public Transform equippedPowerUpList; // Parent transform for the equipped power-up list

    public GameObject powerUpDetailsContent;
    public TMP_Text powerUpNameText;
    public TMP_Text powerUpDescriptionText;
    public TMP_Text powerUpUpgradeText;

    public Button equipButton;
    public Button dequipButton;
    public Button levelUpButton;
    public Button levelDownButton;

    private PlayerStatusEffectSO selectedPowerUp;

    void Start()
    {
        PopulatePowerUpList();
        UpdateEquippedPowerUpsUI();
        equipButton.onClick.AddListener(EquipSelectedPowerUp);
        dequipButton.onClick.AddListener(() => DequipPowerUp(selectedPowerUp));
    }

    void PopulatePowerUpList()
    {
        foreach (var powerUp in availablePowerUps.powerups)
        {
            GameObject buttonObj = Instantiate(powerUpButtonPrefab, powerUpListContent);
            PlayerPowerupsUIItem button = buttonObj.GetComponent<PlayerPowerupsUIItem>();
            button.SetEquippedStatus(powerups.powerups.Contains(powerUp)); // Set initial status
            button.SetUp(powerUp);
            button.OnPowerUpSelected += ShowPowerUpDetails;
        }
    }

    public void ShowPowerUpDetails(PlayerStatusEffectSO powerUp)
    {
        selectedPowerUp = powerUp;
        powerUpDetailsContent.SetActive(true);
        UpdatePowerUpDetails();
    }

    public void UpdatePowerUpDetails()
    {
        powerUpNameText.text = selectedPowerUp.effectName + "\nLv. " + selectedPowerUp.level;
        powerUpDescriptionText.text = selectedPowerUp.description;
        // powerUpDurationText.text = "Duration: " + powerUp.duration;
        levelUpButton.interactable = selectedPowerUp.level < selectedPowerUp.maxLevel;
        levelDownButton.interactable = selectedPowerUp.level > 1;
        bool isSelectedPowerupEquipped = powerups.powerups.Contains(selectedPowerUp);
        equipButton.gameObject.SetActive(!isSelectedPowerupEquipped);
        dequipButton.gameObject.SetActive(isSelectedPowerupEquipped);
    }

    public void EquipSelectedPowerUp()
    {
        if (selectedPowerUp != null && !powerups.powerups.Contains(selectedPowerUp))
        {
            powerups.powerups.Add(selectedPowerUp);
            UpdateEquippedPowerUpsUI();
            UpdateEquippableButtonsStatus();
            UpdatePowerUpDetails();
        }
    }

    public void DequipPowerUp(PlayerStatusEffectSO powerUp)
    {
        if (powerups.powerups.Contains(powerUp))
        {
            powerups.powerups.Remove(powerUp);
            UpdateEquippedPowerUpsUI();
            UpdateEquippableButtonsStatus(); 
            UpdatePowerUpDetails();
        }
    }

    void UpdateEquippedPowerUpsUI()
    {
        // Clear existing buttons
        foreach (Transform child in equippedPowerUpList)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons for each equipped power-up
        foreach (var powerUp in powerups.powerups)
        {
            if (powerUp.type == PlayerStatusEffectSO.EffectType.Permanent) continue;
            GameObject buttonObj = Instantiate(equippedPowerUpButtonPrefab, equippedPowerUpList);
            PlayerPowerupsUIEquippedItem button = buttonObj.GetComponent<PlayerPowerupsUIEquippedItem>();
            button.SetUp(powerUp, this);
        }
    }

    private void UpdateEquippableButtonsStatus()
    {
        foreach (Transform child in powerUpListContent) // Iterate through the buttons in the list
        {
            PlayerPowerupsUIItem button = child.GetComponent<PlayerPowerupsUIItem>();
            if (button != null)
            {
                button.SetEquippedStatus(powerups.powerups.Contains(button.powerUp));
            }
        }
    }

    public void LevelUpSelectedPowerUp()
    {
        selectedPowerUp.level ++;
        selectedPowerUp.level = Mathf.Clamp(selectedPowerUp.level, 1, selectedPowerUp.maxLevel);
        UpdatePowerUpDetails();
    }

    public void LevelDownSelectedPowerUp()
    {
        selectedPowerUp.level --;
        selectedPowerUp.level = Mathf.Clamp(selectedPowerUp.level, 1, selectedPowerUp.maxLevel);
        UpdatePowerUpDetails();
    }
}

