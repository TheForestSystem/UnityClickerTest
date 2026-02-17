using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] int updatesPerSeccond = 5;

    [Header("Snowflake Bonus Settings")]
    [SerializeField] Snowflake snowflakePrefab;
    [SerializeField] RectTransform snowflakeBounds;
    [SerializeField] float minSpawnTime = 120f; // 2 minutes
    [SerializeField] float maxSpawnTime = 300f; // 5 minutes


    [HideInInspector] public float count = 0;

    float nextTimeCheck = 1;
    float lastIncomeValue = 0;

    [HideInInspector] public List<StoreUpgrade> storeUpgrades = new List<StoreUpgrade>();

    private void Start()
    {
        UpdateUI();
        Debug.Log("GameManager upgrades count: " + storeUpgrades.Count);

        StartCoroutine(SnowflakeSpawner());

        // wait 5 sec then spawn a snowflake for testing

        Invoke(nameof(SpawnSnowflake), 5f);
    }

    private void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSeccond);
        }
    }

    IEnumerator SnowflakeSpawner()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnSnowflake();
        }
    }

    void SpawnSnowflake()
    {
        Snowflake snowflake = Instantiate(snowflakePrefab, snowflakeBounds);

        snowflake.Initialize(this); // pass GameManager reference
    }

    public RectTransform GetSnowflakeBounds() => snowflakeBounds;

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

    public void AddCount(float amount)
    {
        count += amount;
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
