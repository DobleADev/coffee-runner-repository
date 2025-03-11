using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldMenuManager : MonoBehaviour
{
    [SerializeField] private WorldDataSO[] _worlds;
    [SerializeField] private WorldDataSO _selectedWorld;
    [SerializeField] private WorldMenuItem _itemPrefab;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private UnityEvent _onWorldSelected;
    List<GameObject> _items = new List<GameObject>();

    void OnEnable()
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        foreach (var child in _items)
        {
            Destroy(child);
        }

        // Create new buttons for each equipped power-up
        foreach (var world in _worlds)
        {
            WorldMenuItem worldItem = Instantiate(_itemPrefab, _itemParent);
            worldItem.SetUp(world, this);
            _items.Add(worldItem.gameObject);
        }
    }

    internal void SelectWorld(WorldDataSO world)
    {
        _selectedWorld.SetData(world);
        _onWorldSelected.Invoke();
    }
}
