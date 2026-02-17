using System.Collections;
using TMPro;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] float fallSpeed = 120f;
    [SerializeField] float swayAmount = 80f;
    [SerializeField] float swaySpeed = 1.5f;

    [Header("Components")]
    [SerializeField] GameObject SnowflakeGraphic;
    [SerializeField] GameObject TextObject;
    [SerializeField] TMP_Text bonusText;

    private RectTransform rect;
    private RectTransform boundsRect;
    private float startX;
    private float timeOffset;
    private GameManager gameManager;
    private int bonusValue;

    public void Initialize(GameManager gm)
    {
        gameManager = gm;
        boundsRect = gm.GetSnowflakeBounds();
        bonusValue = Random.Range(1, 10) * 5;
    }

    public void Initialize(GameManager gm, int bonus)
    {
        gameManager = gm;
        boundsRect = gm.GetSnowflakeBounds();
        bonusValue = bonus;
    }

    void Start()
    {
        rect = GetComponent<RectTransform>();
        bonusText.text = "+" + bonusValue.ToString();
        SnowflakeGraphic.SetActive(true);
        TextObject.SetActive(false);
        Spawn();
    }

    void Update()
    {
        // Fall down
        rect.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

        // Side-to-side drift
        float sway = Mathf.Sin((Time.time + timeOffset) * swaySpeed) * swayAmount;
        rect.anchoredPosition = new Vector2(startX + sway, rect.anchoredPosition.y);

        // Rotate slightly
        rect.Rotate(0, 0, 20f * Time.deltaTime);

        if (rect.anchoredPosition.y < -boundsRect.rect.height * 0.5f - rect.rect.height)
        {
            Destroy(gameObject);
        }
    }

    public void OnClick()
    {
        StartCoroutine(PopAndDie());
    }

    void Spawn()
    {
        float halfBoundsW = boundsRect.rect.width * 0.5f;
        float halfBoundsH = boundsRect.rect.height * 0.5f;
        float halfFlakeW = rect.rect.width * 0.5f;
        float halfFlakeH = rect.rect.height * 0.5f;

        float x = Random.Range(-halfBoundsW + halfFlakeW, halfBoundsW - halfFlakeW);
        float y = halfBoundsH + halfFlakeH;

        rect.anchoredPosition = new Vector2(x, y);
        startX = x;
        timeOffset = Random.Range(0f, 100f);

        fallSpeed = Random.Range(80f, 180f);
        swayAmount = Random.Range(30f, 120f);
        swaySpeed = Random.Range(0.5f, 2.5f);
    }

    IEnumerator PopAndDie()
    {
        Vector2 start = rect.anchoredPosition;
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = start + Vector2.up * t * 50f;
            yield return null;
        }
        SnowflakeGraphic.SetActive(false);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameManager.AddCount(bonusValue);

        Destroy(gameObject);
    }

}
