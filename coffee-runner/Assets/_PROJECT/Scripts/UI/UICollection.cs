using System.Collections.Generic;
using UnityEngine;

public abstract class UICollection<T> : MonoBehaviour where T : UIItem
{
    [SerializeField] T _prefab;
    [SerializeField] protected RectTransform _parent;
    [SerializeField] protected List<T> _items = new List<T>();

    public void UpdateUI()
    {
        for (int i = _parent.childCount - 1; i >= 0; i--)
        {
            Destroy(_parent.GetChild(i).gameObject);
        }
        _items.Clear();

        for (int i = 0; i < GetItemsCount(); i++)
        {
            var item = Instantiate(_prefab, _parent);
            AddItem(item, i);
            _items.Add(item);
        }

    }

    protected abstract int GetItemsCount();
    protected abstract void AddItem(T item, int index);
}

public abstract class UIItem : MonoBehaviour { }
