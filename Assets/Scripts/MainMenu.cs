using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _titleCanvasGroup;
    [SerializeField] private CanvasGroup _catSelectionCanvasGroup;
    [SerializeField] private GameObject _gatchaMenu;
    [SerializeField] private Button _gatchaButton;
    [SerializeField] private TMP_Text _gatchaPulls;


    private void Awake()
    {
        _gatchaMenu.SetActive(false);
        _titleCanvasGroup.alpha = 1f;
        _titleCanvasGroup.blocksRaycasts = true;
        _titleCanvasGroup.interactable = true;

        _catSelectionCanvasGroup.alpha = 0f;
        _catSelectionCanvasGroup.blocksRaycasts = false;
        _catSelectionCanvasGroup.interactable = false;

        if (InterSceneData.GameStarted)
        {
            Click();
        }
        else
        {
            InterSceneData.GameStarted = true;
        }
    }

    private void Update()
    {
        _gatchaPulls.text = $"{InterSceneData.Pulls}";
    }

    public void Click()
    {
        _gatchaMenu.SetActive(true);
        if (InterSceneData.Pulls < 1)
        {
            _gatchaButton.interactable = false;
        }
        _titleCanvasGroup.alpha = 0f;
        _titleCanvasGroup.blocksRaycasts = false;
        _titleCanvasGroup.interactable = false;

        _catSelectionCanvasGroup.alpha = 1f;
        _catSelectionCanvasGroup.blocksRaycasts = true;
        _catSelectionCanvasGroup.interactable = true;
    }

    public void GatchaClick()
    {
        InterSceneData.Pulls--;
        SceneManager.LoadScene("GatchaAnimation");
    }
}
