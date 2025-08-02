using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatchaController : MonoBehaviour
{
    [SerializeField] private Material _GatchaCat;
    [SerializeField] private Light _GatchaLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float gatchaRoll = Random.value;

        if (gatchaRoll <= 0.35)
        {
            // 3 Star Junk
            _GatchaLight.color = new Color(0.0f,0.5f,1.0f);
        }
        else if (gatchaRoll <= 0.5)
        {
            // Oreo Cat
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else if (gatchaRoll <= 0.75)
        {
            // Honey Cat
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else if (gatchaRoll <= 0.9)
        {
            // Apple Cat
            _GatchaLight.color = new Color(1.0f,0.0f,1.0f);
        }
        else
        {
            // Froot Cat
            _GatchaLight.color = new Color(1.0f,1.0f,0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
