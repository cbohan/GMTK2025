using UnityEngine;

public class RacePoint : MonoBehaviour
{
    private const float DISTANCE_BETWEEN_CATS = .05f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, .25f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }

    public Vector3 GetPosition(int index)
    {
        return transform.position + (transform.forward * DISTANCE_BETWEEN_CATS * index);
    }
}
