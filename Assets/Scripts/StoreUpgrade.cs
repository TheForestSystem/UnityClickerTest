using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{

    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Image iconImage;
    [SerializeField] Button button;
    UpgradeData data;
    GameManager gameManager;

    int level;

    public void Initialize(UpgradeData upgrade, GameManager gm)
    {
        data = upgrade;
        gameManager = gm;

        nameText.text = data.upgradeName;
        iconImage.sprite = data.icon;

        button.onClick.AddListener(Buy);

        UpdateUI();
    }

    public void Buy()
    {
        int price = CalculatePrice();
        if (gameManager.PurchaseAction(price))
        {
            level++;
            UpdateUI();
        }
    }

    int CalculatePrice()
    {
        return Mathf.RoundToInt(data.startPrice * Mathf.Pow(data.priceMultiplier, level));
    }

    public float CalculateIncomePerSeccond() { return data.cookiesPerUpgrade * level; }

    public float GetPassiveIncome()
    {
        return data.stats.passiveIncome * level;
    }

    public float GetAutoClicks()
    {
        return data.stats.autoClicksPerSecond * level;
    }

    public float GetClickMultiplier()
    {
        return data.stats.clickMultiplier * level;
    }
    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeText.text = $"{level} x {data.cookiesPerUpgrade}/s";
        button.interactable = gameManager.count >= CalculatePrice();

        bool isPurchased = level > 0;
        iconImage.color = isPurchased ? Color.white : Color.black;
        nameText.text = isPurchased ? data.upgradeName : "???";
    }
}
