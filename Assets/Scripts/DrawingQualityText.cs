using System.Collections;
using TMPro;
using UnityEngine;

public class DrawingQualityText : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;

    public void Init(string text)
    { 
        GetComponent<TMP_Text>().text = text;
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float remainingTime = .75f;

        while (remainingTime > 0)
        { 
            _canvasGroup.alpha = remainingTime * 2f;
            _rectTransform.anchoredPosition = new Vector2(
                _rectTransform.anchoredPosition.x, 
                _rectTransform.anchoredPosition.y + (50f * Time.deltaTime));
            remainingTime -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
