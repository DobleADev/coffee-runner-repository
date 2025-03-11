using TMPro;
using UnityEngine;

public class WorldMenuItem : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private WorldDataSO world;
    [SerializeField] private WorldMenuManager window;

    public void SetUp(WorldDataSO world, WorldMenuManager window)
    {
        this.world = world;
        this.window = window;
        label.text = world.worldName;
    }

    public void OnClick()
    {
        window.SelectWorld(world);
    }
}
