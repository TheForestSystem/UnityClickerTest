using UnityEngine;

[CreateAssetMenu(menuName = "Clicker/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [Header("Basic Info")]
    public string upgradeName;
    public Sprite icon;

    [Header("Pricing")]
    public int startPrice;
    public float priceMultiplier;
    public float cookiesPerUpgrade;

    [Header("Stats")]
    public UpgradeStats stats;
}
