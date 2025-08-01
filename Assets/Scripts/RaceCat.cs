using UnityEngine;

public class RaceCat : MonoBehaviour
{
    private const float ANIMATION_SPEED = 2f;

    public Vector3 Position
    { 
        get {  return transform.position; }
        set { transform.position = value; }
    }

    [Header("Input")]
    public bool IsPlayerControlled;

    [Header("Stats")]
    public float TrotSpeedMult = .5f;
    public float MaxSpeed = 5f;
    public float SlowdownRate = .1f;
    public float Acceleration =  2f;

    [Header("Animations")]
    public Texture[] SideViewRunFrames;

    [Header("References")]
    public Transform VisualsTransform;
    public MeshRenderer SideRenderer;

    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;

    private float _animationTime;

    private void Update()
    {
        CalculateSpeed();
        UpdateVisuals();
        UpdateUI();
    }

    public void SpeedUp(float amount)
    {
        CurrentSpeed += Acceleration * amount;
    }

    private void CalculateSpeed()
    {
        if (CurrentSpeed > MaxSpeed * TrotSpeedMult)
        { 
            CurrentSpeed -= SlowdownRate * Time.deltaTime;
        }

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0f, MaxSpeed);
    }


    private void UpdateVisuals()
    {
        Vector3 cameraPosition = new Vector3(
            Camera.main.transform.position.x,
            transform.position.y,
            Camera.main.transform.position.z);

        Vector3 forward = NextPoint.Position - LastPoint.Position;
        if (forward.magnitude > 0f)
        {
            VisualsTransform.forward = forward;
        }

        _animationTime += Time.deltaTime * CurrentSpeed * ANIMATION_SPEED;
        int frame = Mathf.FloorToInt(_animationTime) % SideViewRunFrames.Length;
        SideRenderer.material.SetTexture("_BaseMap", SideViewRunFrames[frame]);
    }

    private void UpdateUI()
    {
        if (!IsPlayerControlled) return;

        UIHUD.Instance.SpeedSlider.value = CurrentSpeed / MaxSpeed;
    }
}
