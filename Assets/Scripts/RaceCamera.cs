using UnityEngine;

public class RaceCamera : MonoBehaviour
{
    public Vector2 PositionXRange = new Vector2(0f, 0f);

    private void LateUpdate()
    {
        Vector3 position = RaceManager.Instance.PlayerCat.Position;
        Vector3 moveTowards = new Vector3(
            Mathf.Clamp(position.x, PositionXRange.x, PositionXRange.y),
            0f,
            0f);
        position = Vector3.MoveTowards(position, moveTowards, 5f);

        transform.position = position + Vector3.up;
        transform.LookAt(RaceManager.Instance.PlayerCat.Position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.right * PositionXRange.x, Vector3.right * PositionXRange.y);
    }
}
