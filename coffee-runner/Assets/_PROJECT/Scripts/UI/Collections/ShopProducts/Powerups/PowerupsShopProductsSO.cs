using UnityEngine;

[CreateAssetMenu(fileName = "PowerupsShopProduct", menuName = "Scriptable Objects/Shop Product/Powerup")]
public class PowerupsShopProductsSO : ShopProductSO
{
    public PlayerStatusEffectSO powerup;
    public bool onlyUpgreadable;
    public IntUpgradable upgradePrice; // Precio para mejorar el powerup
    public override string GetName()
    {
        return powerup.effectName;
    }
}

[System.Serializable]
public struct IntUpgradable : IUpgradable<int>
{
    [SerializeField] int _baseValue;
    [SerializeField] UpgradeData<int>[] _upgrades;
    public int baseValue { get { return _baseValue; } set { _baseValue = value; } }
    UpgradeData<int>[] IUpgradable<int>.upgrades { get { return _upgrades; } set { _upgrades = value; } }

    public int Value(int level)
    {
        if (_upgrades == null) return _baseValue;
        if (_upgrades.Length == 0) return _baseValue;
        if (level > 1)
        {
            UpgradeData<int> upgrade = _upgrades[Mathf.Min(level - 2, _upgrades.Length - 1)];
            return _baseValue + upgrade.Value(_baseValue);
        }
        return _baseValue;
    }
}