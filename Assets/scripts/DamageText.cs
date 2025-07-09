using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float floatUpSpeed = 50f;

    public float fadeDuration = 0.5f;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // Update is called once per frame
    public void show(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(FloatUp());
    }
    private IEnumerator FloatUp()
    {
    float elapsed = 0f; // 경과 시간 초기화

    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime; // 시간 누적

        rect.anchoredPosition += Vector2.up * floatUpSpeed * Time.deltaTime; // 위로 부드럽게 이동
        canvasGroup.alpha = 1 - (elapsed / fadeDuration); // 점점 투명하게

        yield return null; // 다음 프레임까지 대기
    }

    Destroy(gameObject); // 애니메이션 후 오브젝트 삭제
    }
}
