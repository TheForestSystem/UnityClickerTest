using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] Button button;
    [SerializeField] Image charImage;
    [SerializeField] TMP_Text upgradeNameText;

    [Header("Generator Values")]
    [SerializeField] int startPrice;
    [SerializeField] float upgradePriceMultiplier;
    [SerializeField] float cookiesPerUpgrade;

    [Header("Managers")]
    [SerializeField] GameManager gameManager;

    int level = 0;
    string upgradeName;

    private void Start()
    {
        upgradeName = upgradeNameText.text;
        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if ( purchaseSuccess)
        {
            level++;
            UpdateUI();
        }
    }

    int CalculatePrice()
    {
        return Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
    }

    public float CalculateIncomePerSeccond()
    {
        return cookiesPerUpgrade * level;
    }

    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeText.text = level.ToString() + " x " + cookiesPerUpgrade.ToString() + "/s";

        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;

        bool isPurchased = level > 0;
        charImage.color = isPurchased ? Color.white : Color.black;
        upgradeNameText.text = isPurchased ? upgradeName : "???";
    }
}
