using UnityEngine;
using UnityEngine.UI;

public class PlayerEffectUIItem : MonoBehaviour
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _timeRemainingText;
    
    public void UpdateContents(string name, string timeRemaining)
    {
        _nameText.text = name;
        _timeRemainingText.text = timeRemaining;
    }
}
