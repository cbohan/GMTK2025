using UnityEngine;

public class MenuMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _titleCanvasGroup;
    [SerializeField] private CanvasGroup _catSelectionCanvasGroup;


    private void Awake()
    {
        _titleCanvasGroup.alpha = 1f;
        _titleCanvasGroup.blocksRaycasts = true;
        _titleCanvasGroup.interactable = true;

        _catSelectionCanvasGroup.alpha = 0f;
        _catSelectionCanvasGroup.blocksRaycasts = false;
        _catSelectionCanvasGroup.interactable = false;
    }

    public void Click()
    {
        _titleCanvasGroup.alpha = 0f;
        _titleCanvasGroup.blocksRaycasts = false;
        _titleCanvasGroup.interactable = false;

        _catSelectionCanvasGroup.alpha = 1f;
        _catSelectionCanvasGroup.blocksRaycasts = true;
        _catSelectionCanvasGroup.interactable = true;
    }
}
