using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private WorldDataSO _selectedWorld;
    [SerializeField] private LevelDataSO _selectedLevel;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private LevelMenuItem _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private UnityEvent _onLevelSelected;
    List<GameObject> _items = new List<GameObject>();
    
    void OnEnable()
    {
        _title.text = _selectedWorld.worldName;
        UpdateItems();
    }

    public void UpdateItems()
    {
        foreach (var child in _items)
        {
            Destroy(child);
        }

        // Create new buttons for each equipped power-up
        foreach (var level in _selectedWorld.levels)
        {
            var levelItem = Instantiate(_itemPrefab, _itemParent);
            levelItem.SetUp(level, this);
            _items.Add(levelItem.gameObject);
        }
    }

    internal void SelectWorld(LevelDataSO level)
    {
        _selectedLevel.SetData(level);
        _onLevelSelected.Invoke();
    }
}
