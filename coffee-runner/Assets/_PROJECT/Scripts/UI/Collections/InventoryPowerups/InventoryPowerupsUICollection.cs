using UnityEngine;
using System.Collections.Generic;

public class InventoryPowerupsUICollection : UICollection<InventoryPowerupsUIItem>
{
    [SerializeField] private InventoryPowerupsUIDetails _detailsPanel;

    protected override int GetItemsCount()
    {
        return GameDataManager.instance.GetOwnedPowerupsCount();
    }


    protected override void AddItem(InventoryPowerupsUIItem item, int index)
    {
        var powerup = GameDataManager.instance.GetOwnedPowerup(index);
        item.Setup(powerup, () => ShowDetails(powerup));
    }

    private void ShowDetails(PlayerStatusEffectOwned powerup)
    {
        _detailsPanel.Setup(powerup);
    }
}