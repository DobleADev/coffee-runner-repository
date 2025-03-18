using UnityEngine;

public class PreparationPowerupsUICollection : UICollection<PreparationPowerupsUIItem>
{
    [SerializeField] PlayerStatusEffectContainerSO _equippedPowerups;
    protected override void AddItem(PreparationPowerupsUIItem item, int index)
    {
        item.Setup(GameDataManager.instance.GetOwnedPowerup(index));
    }

    protected override int GetItemsCount()
    {
        return GameDataManager.instance.GetOwnedPowerupsCount();
    }

    public void ApplyEquippedPowerups()
    {
        _equippedPowerups.powerups = GameDataManager.instance.EquipOwnedPowerups();
    }
}
