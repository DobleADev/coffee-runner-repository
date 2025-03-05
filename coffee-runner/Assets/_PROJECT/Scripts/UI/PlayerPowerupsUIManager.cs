using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupsUIManager : MonoBehaviour
{
    public PlayerEquippedPowerups availablePowerUps; // List of all equippable power-ups
    public PlayerEquippedPowerups powerups; // Scriptable object for equipped power-ups

    public GameObject powerUpButtonPrefab; // Prefab for equippable power-up buttons
    public Transform powerUpListContent; // Parent transform for the equippable power-up list

    public GameObject equippedPowerUpButtonPrefab; // Prefab for equipped power-up buttons
    public Transform equippedPowerUpList; // Parent transform for the equipped power-up list

    public GameObject powerUpDetailsContent;
    public TMP_Text powerUpNameText;
    public TMP_Text powerUpDescriptionText;
    public TMP_Text powerUpLevelText;
    public TMP_Text powerUpDurationText;

    public Button equipButton;
    public Button dequipButton;

    private PlayerEffectSO selectedPowerUp;

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
            button.SetUp(powerUp, this);
        }
    }

    public void ShowPowerUpDetails(PlayerEffectSO powerUp)
    {
        selectedPowerUp = powerUp;
        powerUpNameText.text = powerUp.effectName;
        powerUpDescriptionText.text = powerUp.description;
        // powerUpLevelText.text = "Level: " + powerUp.currentLevel;
        // powerUpDurationText.text = "Duration: " + powerUp.duration;
        bool isSelectedPowerupEquipped = powerups.powerups.Contains(powerUp);
        equipButton.gameObject.SetActive(!isSelectedPowerupEquipped);
        dequipButton.gameObject.SetActive(isSelectedPowerupEquipped);
        powerUpDetailsContent.SetActive(true);
    }

    public void EquipSelectedPowerUp()
    {
        if (selectedPowerUp != null && !powerups.powerups.Contains(selectedPowerUp))
        {
            powerups.powerups.Add(selectedPowerUp);
            UpdateEquippedPowerUpsUI();
            UpdateEquippableButtonsStatus(); // Update the status of the equippable buttons
            
            bool isSelectedPowerupEquipped = powerups.powerups.Contains(selectedPowerUp);
            equipButton.gameObject.SetActive(!isSelectedPowerupEquipped);
            dequipButton.gameObject.SetActive(isSelectedPowerupEquipped);
        }
    }

    public void DequipPowerUp(PlayerEffectSO powerUp)
    {
        if (powerups.powerups.Contains(powerUp))
        {
            powerups.powerups.Remove(powerUp);
            UpdateEquippedPowerUpsUI();
            UpdateEquippableButtonsStatus(); // Update the status of the equippable buttons

            bool isSelectedPowerupEquipped = powerups.powerups.Contains(selectedPowerUp);
            equipButton.gameObject.SetActive(!isSelectedPowerupEquipped);
            dequipButton.gameObject.SetActive(isSelectedPowerupEquipped);
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
}

