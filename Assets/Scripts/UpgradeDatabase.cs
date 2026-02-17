using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Clicker/Upgrade Database")]
public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> upgrades;
}
