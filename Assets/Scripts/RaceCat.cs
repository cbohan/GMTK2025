using UnityEngine;
using UnityEngine.InputSystem;

public class RaceCat : MonoBehaviour
{
    private const float SPEED_UP_STAMINA_COST = .1f;
    private const float ANIMATION_SPEED = 2f;

    public Vector3 Position
    { 
        get {  return transform.position; }
        set { transform.position = value; }
    }

    [Header("Input")]
    public bool IsPlayerControlled;
    public InputActionReference MoveInput;

    [Header("Stats")]
    public float MaxSpeed = 5f;
    public float SlowdownRate = 1f;
    public float Acceleration = .5f;
    public float MaxStamina = 1f;
    public float StaminaRecovery = .15f;

    [Header("Fine Tuning")]
    public AnimationCurve SpeedUpStaminaCostCurve;

    [Header("Animations")]
    public Texture[] SideViewRunFrames;

    [Header("References")]
    public Transform VisualsTransform;
    public MeshRenderer SideRenderer;

    [HideInInspector] public float CurrentSpeed = 0f;
    [HideInInspector] public float CurrentStamina = 1f; 

    [HideInInspector] public RacePoint LastPoint;
    [HideInInspector] public RacePoint NextPoint;

    private float _animationTime;

    private bool _spedUpThisFrame;

    private void Update()
    {
        _spedUpThisFrame = false;
        PlayerControl();
        CalculateSpeed();
        UpdateStamina();
        UpdateVisuals();
        UpdateUI();
    }

    private void PlayerControl()
    {
        if (!IsPlayerControlled) return;

        float staminaCostToSpeedUp = SPEED_UP_STAMINA_COST * SpeedUpStaminaCostCurve.Evaluate(CurrentSpeed / MaxSpeed);

        if (
            MoveInput.action.WasPressedThisFrame() &&
            CurrentStamina >= staminaCostToSpeedUp)
        {
            _spedUpThisFrame = true;
            CurrentStamina -= staminaCostToSpeedUp;
        }
    }

    private void CalculateSpeed()
    {
        if (_spedUpThisFrame)
        {
            CurrentSpeed += Acceleration;
        }
        else
        {
            CurrentSpeed -= SlowdownRate * Time.deltaTime;
        }

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0f, MaxSpeed);
    }

    private void UpdateStamina()
    {
        CurrentStamina += StaminaRecovery * Time.deltaTime;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0f, MaxStamina);
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
        UIHUD.Instance.StaminaSlider.value = CurrentStamina / MaxStamina;
    }
}
