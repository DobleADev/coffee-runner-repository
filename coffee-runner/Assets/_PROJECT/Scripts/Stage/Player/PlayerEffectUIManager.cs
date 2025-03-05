using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffectUIManager : MonoBehaviour
{
    public PlayerController player;
    public PlayerEquippedPowerups equippedPowerups;
    public Text playerHealthText;
    public Slider playerHealthBar;
    public TMP_Text environmentTemperature;
    public PlayerEffectUIItem effectUIPrefab;
    public Transform effectUIParent;
    public PlayerEquippedEffectUIItem equippedPowerupsPrefab;
    public Transform equippedPowerupsParent;

    private List<GameObject> effectUIElements = new List<GameObject>();
    private List<GameObject> equippedEffectUIElements = new List<GameObject>();

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (player == null)
        {
            return;
        }

        UpdateTemperatureUI();
        UpdateEffectUI();
    }

    private void UpdateTemperatureUI()
    {
        playerHealthBar.value = player.Temperature;
        playerHealthText.text = player.Temperature.ToString("N1");

        float environmentTemperatureValue = 0;
        for (int i = 0; i < player.activePermanentEffects.Count; i++)
        {
            var effect = player.activePermanentEffects[i];
            if (effect is TemperatureSettingPlayerEffectSO)
            {
                environmentTemperatureValue += (effect as TemperatureSettingPlayerEffectSO).temperature;
            }
            
        }
        environmentTemperature.text = environmentTemperatureValue.ToString("N1");
    }

    void UpdateEffectUI()
    {
        foreach (var effectUI in effectUIElements)
        {
            Destroy(effectUI);
        }
        effectUIElements.Clear();

        foreach (var effect in player.activeEffects)
        {
            PlayerEffectUIItem newEffectUI = Instantiate(effectUIPrefab, effectUIParent);
            TimeSpan time = TimeSpan.FromSeconds(effect.timeRemaining);
            newEffectUI.UpdateContents(effect.effect.effectName, time.ToString(@"mm\:ss"));
            effectUIElements.Add(newEffectUI.gameObject);
        }
    }

    public void UpdateEquippedEffectUI()
    {
        foreach (var effectUI in equippedEffectUIElements)
        {
            Destroy(effectUI);
        }
        equippedEffectUIElements.Clear();

        foreach (var effect in equippedPowerups.powerups)
        {
            if (effect is PermanentEffectSO) continue;
            PlayerEquippedEffectUIItem newEffectUI = Instantiate(equippedPowerupsPrefab, equippedPowerupsParent);
            // TimeSpan time = TimeSpan.FromSeconds(effect.timeRemaining);
            // newEffectUI.UpdateContents(effect.effect.effectName, time.ToString(@"mm\:ss"));
            newEffectUI.equippedEffect = effect;
            newEffectUI.player = player;
            newEffectUI.UpdateContents(effect.effectName);
            equippedEffectUIElements.Add(newEffectUI.gameObject);
        }
    }

    
}
