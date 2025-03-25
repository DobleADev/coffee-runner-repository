using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonTabItem : MonoBehaviour
{
    [SerializeField] TMP_Text _tabName;
    [SerializeField] Button _tabButton;

    public void Setup(string name, UnityAction tabAction)
    {
        _tabName.text = name;
        _tabButton.onClick.AddListener(tabAction);
    }

}
