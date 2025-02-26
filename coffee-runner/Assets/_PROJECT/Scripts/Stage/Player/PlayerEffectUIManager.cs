using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffectUIManager : MonoBehaviour
{
    public PlayerController player;
    public Text playerHealthText;
    public Slider playerHealthBar;
    public TMP_Text environmentTemperature;
    public PlayerEffectUIItem effectUIPrefab;
    public Transform effectUIParent;

    private List<GameObject> effectUIElements = new List<GameObject>();

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
}
