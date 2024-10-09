using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/New Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public float upgradeValue;

    public enum UpgradeType
    {
        Health,
        Speed,
        Damage
    }
}
