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
        // foreach (var world in _worlds)
        // {
        //     WorldMenuItem worldItem = Instantiate(_itemPrefab, _itemParent);
        //     worldItem.SetUp(world, this);
        //     _items.Add(worldItem.gameObject);
        // }

        for (int i = 0; i < _worlds.Length; i++)
        {
            var world = _worlds[i];
            WorldMenuItem worldItem = Instantiate(_itemPrefab, _itemParent);
            worldItem.SetUp(world, this);
            if (i > 0)
            {
                // Previous Level data
                // var worldData = GameDataManager.instance.isWorldCompleted(_worlds[i-1].worldName);
                // bool locked = true;
                // if (worldData != null)
                // {
                //     if (worldData.completed)
                //     {
                //         locked = false;
                //     }
                // }

                worldItem.SetLock(!GameDataManager.instance.isWorldCompleted(_worlds[i-1]));
            }
            _items.Add(worldItem.gameObject);
        }
    }

    internal void SelectWorld(WorldDataSO world)
    {
        _selectedWorld.SetData(world);
        _onWorldSelected.Invoke();
    }
}
