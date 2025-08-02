using UnityEngine;
using UnityEngine.InputSystem;

public class RaceCat : MonoBehaviour
{
    private const float SUGAR_RUSH_DURATION = 1f;

    public float ChungusMultiplier { get; private set; } = 1f;

    public Vector3 Position
    { 
        get {  return transform.position; }
        set { transform.position = value; }
    }

    [Header("Input")]
    public bool IsPlayerControlled;
    [SerializeField] private InputActionReference _ultInput;

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
    public MeshRenderer FrontRenderer;
    public MeshRenderer BackRenderer;
    public ImageLookup ImageLookup;

    [HideInInspector] public int Index;
    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;
    [HideInInspector] public bool FinishedRace;

    private float _ultMeter;
    private float _aiBoostTimer;
    private float _rotation;

    private float _surgeSpeedMult = 1f;
    private float _sugarRushTimer = 0f;
    private float _preSugarRushSpeed;

    private UltType _ultType;

    private void Update()
    {
        Ult();
        CalculateSpeed();
        UpdateVisuals();
        UpdateUI();
        AI();
    }

    private void Ult()
    {
        if (!IsPlayerControlled || !_ultInput.action.WasPressedThisFrame()) return;

        if (_ultType == UltType.Sticky_Honey)
        {
            RaceManager.Instance.StickyHoney();
        }
        else if (_ultType == UltType.Sugar_Rush)
        {
            _preSugarRushSpeed = CurrentSpeed;
            _sugarRushTimer = SUGAR_RUSH_DURATION;
        }
        else if (_ultType == UltType.Apple_Jacked)
        {
            _surgeSpeedMult = 2f;
        }
        else if (_ultType == UltType.Chungus_Mode)
        {
            ChungusMultiplier = 1.5f;
        }

        _ultMeter = 0;
        UIHUD.Instance.SetUlt(_ultMeter);
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
        FrontRenderer.material.SetTexture("_BaseMap", ImageLookup.GetRaceTextureFront(data.Image));
        BackRenderer.material.SetTexture("_BaseMap", ImageLookup.GetRaceTextureBack(data.Image));

        _ultType = data.Ult;
    }

    public void Init()
    {
        CurrentSpeed = MaxSpeed * .75f;
        _aiBoostTimer = Random.Range(BoostFrequency.x, BoostFrequency.y);
        UIHUD.Instance.SetUlt(0f);
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

    public void SpeedUp(float amount, bool updatesUltMeter = true)
    {
        if (InterSceneData.PlayerCat.Ability == AbilityType.ShorterLines)
        {
            amount *= Mathf.Pow(.9f, InterSceneData.PlayerCat.Level);
        }
        else if (InterSceneData.PlayerCat.Ability == AbilityType.LongerLines)
        {
            amount *= Mathf.Pow(1.25f, InterSceneData.PlayerCat.Level);
        }

        CurrentSpeed += Acceleration * amount;

        if (IsPlayerControlled && updatesUltMeter)
        {
            _ultMeter += Mathf.Pow(amount, 1.5f) * .3f;
            UIHUD.Instance.SetUlt(Mathf.Clamp01(_ultMeter));
        }
    }

    public void AfterFinishMoveForward()
    {
        FinishedRace = true;

        VisualsParentTransform.transform.position += VisualsParentTransform.forward * CurrentSpeed * Time.deltaTime; 
    }

    private void CalculateSpeed()
    {
        if (FinishedRace) return;

        float trotSpeed = MaxSpeed * TrotSpeedMult;
        if (CurrentSpeed > trotSpeed)
        {
            CurrentSpeed -= SlowdownRate * Time.deltaTime;
            if (CurrentSpeed < trotSpeed) CurrentSpeed = trotSpeed;
        }
        else
        {
            CurrentSpeed += SlowdownRate * 5f * Time.deltaTime;
            if (CurrentSpeed > trotSpeed) CurrentSpeed = trotSpeed;
        }

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0f, MaxSpeed);
        CurrentSpeed *= Mathf.Max(_surgeSpeedMult, 1f);
        if (_sugarRushTimer > 0)
        {
            float sugarRushT = 1f;
            if (_sugarRushTimer < .1f) sugarRushT = _sugarRushTimer * 10f;
            if (_sugarRushTimer > SUGAR_RUSH_DURATION - .1f)
            {
                sugarRushT = (SUGAR_RUSH_DURATION - _sugarRushTimer) * 10f;
            }
            CurrentSpeed = Mathf.Lerp(_preSugarRushSpeed, 10f, sugarRushT);
        }

        _surgeSpeedMult -= Time.deltaTime / 2f;
        _surgeSpeedMult = Mathf.Max(_surgeSpeedMult, 1f);

        _sugarRushTimer -= Time.deltaTime;
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

        float scaleMult = ChungusMultiplier;
        if (IsPlayerControlled)
        {
            scaleMult *= InterSceneData.PlayerCat.Ability == AbilityType.SizeAndSpeedBoost ?
                Mathf.Pow(1.15f, InterSceneData.PlayerCat.Level) :
                1f;
        }
        VisualsTransform.localScale = Vector3.one * scaleMult;
        VisualsTransform.localPosition = Vector3.up * (scaleMult * .5f);
    }

    private void UpdateUI()
    {
        if (!IsPlayerControlled || FinishedRace) return;

        float speedMult = ChungusMultiplier;
        speedMult *= InterSceneData.PlayerCat.Ability == AbilityType.SizeAndSpeedBoost ?
            Mathf.Pow(1.15f, InterSceneData.PlayerCat.Level) :
            1f;
        UIHUD.Instance.SetSpeed(CurrentSpeed * speedMult);
    }
}
