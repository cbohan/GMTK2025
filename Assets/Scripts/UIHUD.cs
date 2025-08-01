using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    public static UIHUD Instance { get; private set; }

    public Slider SpeedSlider;

    private void Awake()
    {
        Instance = this;
    }


}
