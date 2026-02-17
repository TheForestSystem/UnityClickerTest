using UnityEngine;

[CreateAssetMenu(menuName = "Clicker/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;

    public int startPrice;
    public float priceMultiplier;
    public float cookiesPerUpgrade;
}
