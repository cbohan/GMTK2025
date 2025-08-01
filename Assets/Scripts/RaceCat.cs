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

    [Header("References")]
    public Transform VisualsParentTransform;
    public Transform VisualsTransform;
    public MeshRenderer SideRenderer;

    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;

    private float rotation;

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
            VisualsParentTransform.forward = forward;
        }

        rotation += 360f / (2 * Mathf.PI) * CurrentSpeed * Time.deltaTime;
        VisualsTransform.rotation = Quaternion.Euler(
            VisualsTransform.eulerAngles.x, 
            VisualsTransform.eulerAngles.y, 
            rotation);
    }

    private void UpdateUI()
    {
        if (!IsPlayerControlled) return;

        UIHUD.Instance.SetSpeed(CurrentSpeed);
    }
}
