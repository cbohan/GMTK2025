using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    public static UIHUD Instance { get; private set; }

    public Slider SpeedSlider;
    public Slider StaminaSlider;

    private void Awake()
    {
        Instance = this;
    }


}
