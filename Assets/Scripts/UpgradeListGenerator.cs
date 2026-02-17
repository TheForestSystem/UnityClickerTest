using UnityEngine;

public class UpgradeListGenerator : MonoBehaviour
{
    [SerializeField] UpgradeDatabase database;
    [SerializeField] StoreUpgrade upgradePrefab;
    [SerializeField] Transform contentParent;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        Debug.Log("Generating upgrades...");
        gameManager.storeUpgrades.Clear();

        foreach (var upgrade in database.upgrades)
        {
            StoreUpgrade ui = Instantiate(upgradePrefab, contentParent);
            ui.name = upgrade.upgradeName + "Button";
            ui.Initialize(upgrade, gameManager);
            gameManager.storeUpgrades.Add(ui);
            Debug.Log("Added: " + ui.name);
        }
    }

}
