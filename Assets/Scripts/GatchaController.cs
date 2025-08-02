using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatchaController : MonoBehaviour
{
    [SerializeField] private Material _GatchaCat;
    [SerializeField] private Light _GatchaLight;
    [SerializeField] private ImageLookup _imageLookup;
    [SerializeField] private Texture _Poop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float gatchaRoll = Random.value;

        if (gatchaRoll <= 0.35)
        {
            // 3 Star Junk
            _GatchaCat.mainTexture = _Poop;
            _GatchaLight.color = new Color(0.0f,0.5f,1.0f);
        }
        else if (gatchaRoll <= 0.5)
        {
            // Oreo Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Oreo.Image);
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else if (gatchaRoll <= 0.75)
        {
            // Honey Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Honey.Image);
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else if (gatchaRoll <= 0.9)
        {
            // Apple Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Apple.Image);
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else
        {
            // Froot Cat
            _GatchaCat.mainTexture = _imageLookup.GetNonRaceTexture(InterSceneData.Loop.Image);
            _GatchaLight.color = new Color(1.0f,1.0f,0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
