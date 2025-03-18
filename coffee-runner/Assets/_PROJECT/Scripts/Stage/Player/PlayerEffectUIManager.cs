using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffectUIManager : MonoBehaviour
{
    public PlayerController player;
    public LevelController level;
    public PlayerStatusEffectContainerSO equippedPowerups;
    public Text playerHealthText;
    public string playerCurrentSpeedLabel = "Speed";
    public string playerCurrentTemperatureGainLabel = "Temperature Gain";
    public string playerCurrentSlipShieldsLabel = "Slip Shields";
    public string playerCurrentInvensibilityChancesLabel = "Invensibility Chances";
    public TMP_Text playerCurrentSpeedText;
    public TMP_Text playerCurrentTemperatureGainText;
    public TMP_Text playerCurrentSlipShieldsText;
    public TMP_Text playerCurrentInvensibilityChancesText;
    public Slider playerHealthBar;
    public Slider playerProgressBar;
    public TMP_Text environmentTemperature;
    public PlayerEffectUIItem effectUIPrefab;
    public Transform effectUIParent;
    public PlayerEquippedEffectUIItem equippedPowerupsPrefab;
    public Transform equippedPowerupsParent;

    private List<GameObject> effectUIElements = new List<GameObject>();
    private List<GameObject> equippedEffectUIElements = new List<GameObject>();

    private Dictionary<PlayerStatusEffectSO, float> activeEffectDurations = new Dictionary<PlayerStatusEffectSO, float>();

    void LateUpdate()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (player == null)
        {
            return;
        }

        UpdatePlayerStatsUI();
        UpdateEffectUI();
        playerProgressBar.minValue = level.start.transform.position.x;
        playerProgressBar.maxValue = level.end.transform.position.x;
        playerProgressBar.value = player.transform.position.x;
    }

    private void UpdatePlayerStatsUI()
    {
        playerHealthBar.value = player.Temperature;
        playerHealthText.text = player.Temperature.ToString("N1");
        playerCurrentSpeedText.text = playerCurrentSpeedLabel + " = " + player.RunSpeed.ToString("N2");
        playerCurrentTemperatureGainText.text = playerCurrentTemperatureGainLabel + " = " + (player.EffectTemperature + player.EnvironmentTemperature).ToString("N2");
        playerCurrentSlipShieldsText.text = playerCurrentSlipShieldsLabel + " = " + player.CurrentSlipShields.Count.ToString();
        playerCurrentInvensibilityChancesText.text = playerCurrentInvensibilityChancesLabel + " = " +  player.InvencibilityChances.Count.ToString();
        environmentTemperature.text = player.EnvironmentTemperature.ToString("N1");
    }

    public void ApplyEffectDuration(PlayerStatusEffectSO effect, float duration)
    {
        if (activeEffectDurations.ContainsKey(effect))
        {
            activeEffectDurations[effect] = duration;
        }
        else
        {
            activeEffectDurations.Add(effect, duration);
        }
    }

    void UpdateEffectUI()
    {
        foreach (var effectUI in effectUIElements)
        {
            Destroy(effectUI);
        }
        effectUIElements.Clear();

        if (player == null) return; // Asegúrate de que el jugador esté asignado

        foreach (ActiveEffect effect in player.activeEffects)
        {
            PlayerEffectUIItem newEffectUI = Instantiate(effectUIPrefab, effectUIParent);
            TimeSpan time = TimeSpan.FromSeconds(effect.remainingTime); // Accede al valor correctamente
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
            if (effect.type == PlayerStatusEffectSO.EffectType.Permanent) 
            {
                // player.RemoveEffect(effect);
                player.ApplyEffect(effect);
                continue;
            }
            PlayerEquippedEffectUIItem newEffectUI = Instantiate(equippedPowerupsPrefab, equippedPowerupsParent); 
            newEffectUI.equippedEffect = effect;
            newEffectUI.player = player;
            newEffectUI.UpdateContents();
            equippedEffectUIElements.Add(newEffectUI.gameObject);
        }
    }
}