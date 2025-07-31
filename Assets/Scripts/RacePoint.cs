using UnityEngine;

public class RacePoint : MonoBehaviour
{
    public Vector3 Position { get {  return transform.position; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, .25f);
    }
}
