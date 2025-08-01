using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    public static UIHUD Instance { get; private set; }

    [SerializeField] private Transform _dialTransform;
    [SerializeField] private float[] _dialRotations;


    private void Awake()
    {
        Instance = this;
    }

    public void SetSpeed(float speed)
    { 
        int lowerSpeed = Mathf.FloorToInt(speed);
        int upperSpeed = Mathf.FloorToInt(speed) + 1;
        float remainder = speed - lowerSpeed;

        lowerSpeed = Mathf.Clamp(lowerSpeed, 0, _dialRotations.Length - 1);
        upperSpeed = Mathf.Clamp(upperSpeed, 0, _dialRotations.Length - 1);

        float rotation = Mathf.Lerp(_dialRotations[lowerSpeed], _dialRotations[upperSpeed], remainder);
        _dialTransform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
