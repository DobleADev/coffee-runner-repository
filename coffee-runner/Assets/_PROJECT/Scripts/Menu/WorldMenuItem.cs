using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuItem : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Button worldButton;
    [SerializeField] private GameObject detailsObject;
    [SerializeField] private GameObject lockedObject;
    [SerializeField] private WorldDataSO world;
    [SerializeField] private WorldMenuManager window;

    public void SetUp(WorldDataSO world, WorldMenuManager window)
    {
        this.world = world;
        this.window = window;
        label.text = world.worldName;
    }

    public void SetLock(bool locked)
    {
        worldButton.interactable = !locked;
        detailsObject.SetActive(!locked);
        lockedObject.SetActive(locked);
    }

    public void OnClick()
    {
        window.SelectWorld(world);
    }
}
