using UnityEngine;

public class InventoryPowerupsUICollection : UICollection<InventoryPowerupsUIItem>
{
    protected override void AddItem(InventoryPowerupsUIItem item, int index)
    {
        item.Setup();
    }

    protected override int GetItemsCount()
    {
        throw new System.NotImplementedException();
    }
}
