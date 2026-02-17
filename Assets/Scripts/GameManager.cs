using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] int updatesPerSeccond = 5;

    [HideInInspector] public float count = 0;

    float nextTimeCheck = 1;
    float lastIncomeValue = 0;

    [HideInInspector] public List<StoreUpgrade> storeUpgrades = new List<StoreUpgrade>();

    private void Start()
    {
        UpdateUI();
        Debug.Log("GameManager upgrades count: " + storeUpgrades.Count);
    }

    private void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSeccond);
        }
    }

    void IdleCalculate()
    {
        float passiveIncome = 0f;
        float autoClicks = 0f;

        foreach (var u in storeUpgrades)
        {
            if (u == null) continue;

            passiveIncome += u.GetPassiveIncome();
            autoClicks += u.GetAutoClicks();
            u.UpdateUI();
        }

        lastIncomeValue = passiveIncome;

        // Passive income
        count += passiveIncome / updatesPerSeccond;

        // Auto clicks simulate clicking
        float clicksThisTick = autoClicks / updatesPerSeccond;
        count += clicksThisTick;

        UpdateUI();
    }

    public void ClickAction()
    {
        count++;
        count += lastIncomeValue * 0.02f;
        UpdateUI();
    }

    public bool PurchaseAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString() + "/s";
    }
}
