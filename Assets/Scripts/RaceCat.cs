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

    [HideInInspector] public int Index;
    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;

    private float _rotation;
    private bool _finishedRace;

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

    public void AfterFinishMoveForward()
    {
        _finishedRace = true;

        VisualsParentTransform.transform.position += VisualsParentTransform.forward * CurrentSpeed * Time.deltaTime; 
    }

    private void CalculateSpeed()
    {
        if (_finishedRace) return;

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

        Vector3 forward = NextPoint.GetPosition(Index) - LastPoint.GetPosition(Index);
        if (forward.magnitude > 0f)
        {
            VisualsParentTransform.forward = Vector3.Lerp(
                VisualsParentTransform.forward, 
                forward, 
                Time.deltaTime);
        }

        _rotation += 360f / (2 * Mathf.PI) * CurrentSpeed * Time.deltaTime;
        VisualsTransform.rotation =  Quaternion.Euler(
            VisualsTransform.eulerAngles.x,
            VisualsTransform.eulerAngles.y,
            _rotation);

    }

    private void UpdateUI()
    {
        if (!IsPlayerControlled || _finishedRace) return;

        UIHUD.Instance.SetSpeed(CurrentSpeed);
    }
}
