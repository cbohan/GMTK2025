using UnityEngine;

public class RaceCat : MonoBehaviour
{
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

    [Header("AI")]
    public Vector2 BoostFrequency = new Vector2(2f, 5f);
    public Vector2 BoostQuality = new Vector2(0f, 4f);

    [Header("References")]
    public Transform VisualsParentTransform;
    public Transform VisualsTransform;
    public MeshRenderer SideRenderer;
    public ImageLookup ImageLookup;

    [HideInInspector] public int Index;
    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;
    [HideInInspector] public bool FinishedRace;

    private float _aiBoostTimer;
    private float _rotation;

    private void Update()
    {
        CalculateSpeed();
        UpdateVisuals();
        UpdateUI();
        AI();
    }

    public void SetData(CatData data)
    { 
        TrotSpeedMult = .5f;

        MaxSpeed = 5f;
        if (data.Speed == StatValue.Low) MaxSpeed = 4;
        if (data.Speed == StatValue.High) MaxSpeed = 6;

        SlowdownRate = .1f;
        if (data.Stamina == StatValue.Low) SlowdownRate = .15f;
        if (data.Stamina == StatValue.Low) SlowdownRate = .05f;

        Acceleration = 2f;
        if (data.Acceleration == StatValue.Low) Acceleration = 1.5f;
        if (data.Acceleration == StatValue.High) Acceleration = 2.5f;

        SideRenderer.material.SetTexture("_BaseMap", ImageLookup.GetRaceTexture(data.Image));
    }

    public void Init()
    {
        CurrentSpeed = MaxSpeed * .75f;
        _aiBoostTimer = Random.Range(BoostFrequency.x, BoostFrequency.y);
    }

    private void AI()
    {
        if (IsPlayerControlled) return;

        _aiBoostTimer -= Time.deltaTime;

        if (_aiBoostTimer <= 0)
        {
            _aiBoostTimer = Random.Range(BoostFrequency.x, BoostFrequency.y);
            float boostAmount = Random.Range(BoostQuality.x, BoostQuality.y);

            if (boostAmount > 3f)
            {
                SpeedUp(1f);
            }
            else if (boostAmount > 2.5f)
            {
                SpeedUp(.75f);
            }
            else if (boostAmount > 1.5f)
            {
                SpeedUp(.5f);
            }
        }
    }

    public void SpeedUp(float amount)
    {
        CurrentSpeed += Acceleration * amount;
    }

    public void AfterFinishMoveForward()
    {
        FinishedRace = true;

        VisualsParentTransform.transform.position += VisualsParentTransform.forward * CurrentSpeed * Time.deltaTime; 
    }

    private void CalculateSpeed()
    {
        if (FinishedRace) return;

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
        if (!IsPlayerControlled || FinishedRace) return;

        UIHUD.Instance.SetSpeed(CurrentSpeed);
    }
}
