using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectorUI : MonoBehaviour
{
    [SerializeField] GameObject _defaultSkin;
    [SerializeField] TMP_Text _skinNameLabel;
    [SerializeField] Image _skinPreview;
    [SerializeField] Button _equipButton;
    List<GameObject> _skins = new List<GameObject>();
    int _currentIndex = 0;

    void ShowSelectedSkin()
    {
        _skinNameLabel.text = _skins[_currentIndex].name;
        var skinSprite = _skins[_currentIndex].GetComponent<SpriteRenderer>();
        _skinPreview.sprite = skinSprite.sprite;
        _skinPreview.color = skinSprite.color;
        CheckSkinEquipped();
    }

    void CheckSkinEquipped()
    {
        var currentSkin = GameDataManager.instance.currentPlayerSkin;
        _equipButton.interactable = !(currentSkin == null && _skins[_currentIndex] == _defaultSkin) && _skins[_currentIndex] != currentSkin;
    }

    public void UpdateSkinList()
    {
        _skins.Clear();
        _skins.Add(_defaultSkin);
        foreach (var ownedSkin in GameDataManager.instance.ownedSkins)
        {
            _skins.Add(ownedSkin);
        }
        _currentIndex = 0;
        ShowSelectedSkin();
    }

    public void EquipSelected()
    {
        GameDataManager.instance.currentPlayerSkin = _skins[_currentIndex];
        CheckSkinEquipped();
    }

    public void NextSkin()
    {
        _currentIndex += _currentIndex < _skins.Count - 1 ? 1 : -(_skins.Count - 1);
        
        ShowSelectedSkin();
    }

    public void PreviousSkin()
    {
        _currentIndex += _currentIndex > 0 ? -1 : _skins.Count - 1;
        ShowSelectedSkin();
    }
}
