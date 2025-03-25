using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectTabs : MonoBehaviour
{
    public ButtonTabItem _prefab;
    public Transform _parent;
    public List<GameObject> _tabs;
    public List<ButtonTabItem> _items;
    private GameObject _lastOpenedTab;

    public void UpdateUI()
    {
        for (int i = _parent.childCount - 1; i >= 0; i--)
        {
            Destroy(_parent.GetChild(i).gameObject);
        }
        _items.Clear();

        for (int i = 0; i < _tabs.Count; i++)
        {
            int index = i;
            var item = Instantiate(_prefab, _parent);
            item.Setup(_tabs[index].name, () => OpenTab(index));
            _items.Add(item);
        }

        if (_tabs.Count > 0)
        {
            OpenTab(0);
        }
    }

    void OpenTab(int index)
    {
        // Debug.Log(index);
        if (_lastOpenedTab != null)
        {
            _lastOpenedTab.SetActive(false);
        }

        var newTab = _tabs[index];
        newTab.SetActive(true);
        _lastOpenedTab = newTab;
    }
}

// [System.Serializable]
// public class ButtonTab
// {
//     public string Name;
//     public Button Button;
// }