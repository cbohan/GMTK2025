using UnityEngine;
using UnityEngine.UI;

public class UltMeter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _boringText;
    [SerializeField] private GameObject _coolText;

    public void SetUlt(float amount)
    { 
        _slider.value = amount;
        _boringText.SetActive(amount < 1);
        _coolText.SetActive(amount >= 1);
    }
}
