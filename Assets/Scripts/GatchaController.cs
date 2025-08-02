using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GatchaController : MonoBehaviour
{
    [SerializeField] private Material _GatchaCat;
    [SerializeField] private Light _GatchaLight;
    [SerializeField] private ImageLookup _imageLookup;
    [SerializeField] private Texture _Poop;
    [SerializeField] private TMP_Text _GatchaText;
    [SerializeField] private GameObject _GatchaTextEffect;
    [SerializeField] private GameObject _NextButton;

    private string CatText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float gatchaRoll = Random.value;
        
        _NextButton.SetActive(false);

        if (gatchaRoll <= 0.35)
        {
            // 3 Star Junk
            _GatchaCat.mainTexture = _Poop;
            _GatchaLight.color = new Color(0.0f,0.5f,1.0f);
            CatText = "Nothing!";
        }
        else if (gatchaRoll <= 0.5)
        {
            // Oreo Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Oreo.Image);
            if (InterSceneData.Oreo.Level < 4)
            {
                InterSceneData.Oreo.Level += 1;
            }
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
            CatText = "Oreo Cat?!";
        }
        else if (gatchaRoll <= 0.75)
        {
            // Honey Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Honey.Image);
            if (InterSceneData.Honey.Level < 4)
            {
                InterSceneData.Honey.Level += 1;
            }
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
            CatText = "Honey Loops!";
        }
        else if (gatchaRoll <= 0.9)
        {
            // Apple Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Apple.Image);
            if (InterSceneData.Apple.Level < 4)
            {
                InterSceneData.Apple.Level += 1;
            }
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
            CatText = "Jacked Cat!";
        }
        else
        {
            // Froot Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Loop.Image);
            if (InterSceneData.Loop.Level < 4)
            {
                InterSceneData.Loop.Level += 1;
            }
            _GatchaLight.color = new Color(1.0f,1.0f,0.0f);
            CatText = "Brother Loops!";
        }
        StartCoroutine(UpdateText());
    }
    
    private IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(2.3f);
        _GatchaTextEffect.SetActive(false);
        _GatchaText.text = "You Got . . . " + CatText;
        _GatchaTextEffect.SetActive(true);
        _NextButton.SetActive(true);

    }

    public void Click()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
