using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquippedEffectUIItem : MonoBehaviour
{
    [SerializeField] Button _equippedEffectButton;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] Image _cooldownContainer;
    [SerializeField] Image _cooldownFill;
    [SerializeField] TMP_Text _cooldownRemainingText;
    public PlayerController player;
    [SerializeField] private PlayerStatusEffectSO _equippedEffect;
    public PlayerStatusEffectSO equippedEffect { get { return _equippedEffect; } set { _equippedEffect = value; } }

    public void UpdateContents()
    {
        _nameText.text = _equippedEffect.effectName;
    }

    public void UseEquippedEffect()
    {
        player.ApplyEffect(_equippedEffect);
        if (_equippedEffect.useCooldown.Value(_equippedEffect.level) > 0)
        {
            StopAllCoroutines();
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        _cooldownContainer.gameObject.SetActive(true);
        _equippedEffectButton.interactable = false;
        float cooldown = _equippedEffect.useCooldown.Value(_equippedEffect.level);
        float deltaDuration = 1 / cooldown;
        while (cooldown > 0)
        {
            _cooldownFill.fillAmount = cooldown * deltaDuration;
            _cooldownRemainingText.text = cooldown.ToString("N1");
            cooldown -= Time.deltaTime;
            yield return null;
        }
        _cooldownFill.fillAmount = 0f;
        _cooldownRemainingText.text = "";
        _cooldownContainer.gameObject.SetActive(false);
        _equippedEffectButton.interactable = true;
    }
}
