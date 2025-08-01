using UnityEngine;

public class RaceCamera : MonoBehaviour
{
    public Vector2 PositionXRange = new Vector2(0f, 0f);

    [SerializeField] private AnimationCurve _endOfRacePitchCurve;

    private float _endOfRaceAnimationTime = 0;

    private void LateUpdate()
    {
        Vector3 position = RaceManager.Instance.PlayerCat.Position;
        Vector3 moveTowards = new Vector3(
            Mathf.Clamp(position.x, PositionXRange.x, PositionXRange.y),
            0f,
            0f);
        position = Vector3.MoveTowards(position, moveTowards, 5f);
        position.y += 2f;

        transform.position = position + Vector3.up;
        transform.LookAt(RaceManager.Instance.PlayerCat.Position);

        if (RaceManager.Instance.IsRaceFinished)
        {
            _endOfRaceAnimationTime += Time.deltaTime * .5f;
            transform.rotation = Quaternion.Euler(
                Mathf.Lerp(transform.eulerAngles.x, -90, _endOfRacePitchCurve.Evaluate(_endOfRaceAnimationTime)),
                transform.eulerAngles.y, 
                transform.eulerAngles.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.right * PositionXRange.x, Vector3.right * PositionXRange.y);
    }
}
