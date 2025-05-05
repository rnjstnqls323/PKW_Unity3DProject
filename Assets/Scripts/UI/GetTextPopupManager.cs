using TMPro;
using UnityEngine;
using System.Collections;

public class GetTextPopupManager : MonoBehaviour
{
    public static GetTextPopupManager Instance;

    [Header("텍스트 프리팹")]
    [SerializeField] private GameObject expTextPrefab;
    [SerializeField] private GameObject goldTextPrefab;

    [Header("부모 오브젝트")]
    [SerializeField] private Transform textParent;

    [Header("설정")]
    [SerializeField] private float riseHeight = 30f;
    [SerializeField] private float riseTime = 0.4f;
    [SerializeField] private float fadeTime = 1.0f;
    [SerializeField] private Vector3 offset = new Vector3(1.5f, 2f, 0);

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowExpAndGold(float exp, int gold)
    {
        StartCoroutine(ShowSequence(exp, gold));
    }

    private IEnumerator ShowSequence(float exp, int gold)
    {
        Vector3 worldPos = PlayerKnight.Instance.transform.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject expObj = Instantiate(expTextPrefab, textParent);
        expObj.transform.position = screenPos;

        TextMeshProUGUI expText = expObj.GetComponent<TextMeshProUGUI>();
        string expPrefix = expText.text.Split(' ')[0];
        expText.text = $"{expPrefix} {exp.ToString("0.##")}";

        Vector3 startPos = screenPos;
        Vector3 endPos = screenPos + new Vector3(0, riseHeight, 0);
        float elapsed = 0f;
        CanvasGroup expGroup = expObj.GetComponent<CanvasGroup>() ?? expObj.AddComponent<CanvasGroup>();
        expGroup.alpha = 1f;

        while (elapsed < riseTime)
        {
            float t = elapsed / riseTime;
            expObj.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        expObj.transform.position = endPos;

        GameObject goldObj = Instantiate(goldTextPrefab, textParent);
        goldObj.transform.position = endPos + new Vector3(0, -35f, 0);

        TextMeshProUGUI goldText = goldObj.GetComponent<TextMeshProUGUI>();
        string goldPrefix = goldText.text.Split(' ')[0];
        goldText.text = $"{goldPrefix} {gold}";

        CanvasGroup goldGroup = goldObj.GetComponent<CanvasGroup>() ?? goldObj.AddComponent<CanvasGroup>();
        goldGroup.alpha = 1f;

        float fadeElapsed = 0f;
        bool goldFadeStarted = false;

        while (fadeElapsed < fadeTime)
        {
            float t = fadeElapsed / fadeTime;
            expGroup.alpha = 1f - t;

            if (!goldFadeStarted && expGroup.alpha <= 0.5f)
            {
                goldFadeStarted = true;
                StartCoroutine(FadeOutOnly(goldGroup, fadeTime * 0.5f));
            }

            fadeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(expObj);
    }

    private IEnumerator FadeOutOnly(CanvasGroup group, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            group.alpha = 1f - t;
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (group != null && group.gameObject != null)
            Destroy(group.gameObject);
    }
}
