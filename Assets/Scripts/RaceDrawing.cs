using UnityEngine;
using UnityEngine.InputSystem;

public class RaceDrawing : MonoBehaviour
{
    public float LineThickness = 25f;
    public float LineVertexFrequency = 25f;

    [SerializeField] private InputActionReference _mousePosition;
    [SerializeField] private InputActionReference _leftMouse;
    [SerializeField] private Material _lineMaterial;

    private CanvasRenderer _renderer;
    private Mesh _mesh;
    private Vector2 _lastMousePosition;

    private void Awake()
    {
        _renderer = GetComponent<CanvasRenderer>();
    }

    private void Update()
    {
        StartNewLine();
        ContinueLine();
    }

    private void StartNewLine()
    {
        if (!_leftMouse.action.WasPressedThisFrame()) return;

        Vector2 mousePosition = _mousePosition.action.ReadValue<Vector2>();
        _lastMousePosition = mousePosition;

        Vector3[] vertices = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(mousePosition.x, mousePosition.y);
        vertices[1] = new Vector3(mousePosition.x, mousePosition.y);
        vertices[2] = new Vector3(mousePosition.x, mousePosition.y);
        vertices[3] = new Vector3(mousePosition.x, mousePosition.y);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        _mesh = new Mesh();
        _mesh.MarkDynamic();

        _mesh.vertices = vertices;
        _mesh.uv = uvs;
        _mesh.triangles = triangles;
        _mesh.MarkDynamic();

        _renderer.SetMesh(_mesh);
        _renderer.SetMaterial(_lineMaterial, null);
    }

    private void ContinueLine()
    {
        if (!_leftMouse.action.IsPressed()) return;

        Vector2 mousePosition = _mousePosition.action.ReadValue<Vector2>();
        float distanceFromLastPoint = Vector2.Distance(mousePosition, _lastMousePosition);
        if (distanceFromLastPoint < LineVertexFrequency) return;

        Vector3[] vertices = new Vector3[_mesh.vertices.Length + 2];
        Vector2[] uvs = new Vector2[_mesh.uv.Length + 2];
        int[] triangles = new int[_mesh.triangles.Length + 6];

        _mesh.vertices.CopyTo(vertices, 0);
        _mesh.uv.CopyTo(uvs, 0);
        _mesh.triangles.CopyTo(triangles, 0);

        int vIndex = vertices.Length - 4;
        
        Vector2 mouseForward = (mousePosition - _lastMousePosition).normalized;
        Vector2 offsetVector = Rotate(mouseForward, 90f);
        Vector2 upVertex = mousePosition + (offsetVector * LineThickness);
        Vector2 downVertex = mousePosition - (offsetVector * LineThickness);

        vertices[vIndex + 2] = upVertex;
        vertices[vIndex + 3] = downVertex;

        uvs[vIndex + 2] = Vector2.zero;
        uvs[vIndex + 3] = Vector2.zero;

        int tIndex = triangles.Length - 6;
        triangles[tIndex + 0] = vIndex + 0;
        triangles[tIndex + 1] = vIndex + 2;
        triangles[tIndex + 2] = vIndex + 1;

        triangles[tIndex + 3] = vIndex + 1;
        triangles[tIndex + 4] = vIndex + 2;
        triangles[tIndex + 5] = vIndex + 3;

        _mesh.vertices = vertices;
        _mesh.uv = uvs;
        _mesh.triangles = triangles;
        _renderer.SetMesh(_mesh);

        Debug.Log(triangles.Length);

        _lastMousePosition = mousePosition;
    }

    private Vector2 Rotate(Vector2 vector, float angleDegrees)
    {
        // Create a Quaternion representing the rotation around the Z-axis.
        // In 2D, rotations are around the Z-axis (Vector3.forward).
        Quaternion rotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);

        // Multiply the Quaternion by the Vector2. 
        // Unity automatically handles the conversion and rotation.
        return rotation * vector;
    }
}
