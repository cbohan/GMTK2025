using UnityEngine;
using UnityEngine.Events;

public class RaceDrawingTextManager : MonoBehaviour
{
    public static UnityEvent<string, Vector2> OnSpawnText = new UnityEvent<string, Vector2>(); 

    [SerializeField] private DrawingQualityText _drawingQualityTextPrefab;

    private void Awake()
    {
        OnSpawnText.AddListener(SpawnText);    
    }

    private void SpawnText(string text, Vector2 position)
    {
        DrawingQualityText drawingQualityText = Instantiate(_drawingQualityTextPrefab, transform);
        drawingQualityText.GetComponent<RectTransform>().anchoredPosition= position;
        drawingQualityText.Init(text);

    }
}
