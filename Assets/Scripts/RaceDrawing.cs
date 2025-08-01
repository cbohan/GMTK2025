using UnityEngine;
using UnityEngine.InputSystem;

public class RaceDrawing : MonoBehaviour
{
    public float LineThickness = 25f;
    public float LineVertexFrequency = 25f;

    [SerializeField] private InputActionReference _mousePosition;
    [SerializeField] private InputActionReference _leftMouse;
    [SerializeField] private Material _drawMaterial;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private CanvasRenderer _drawRenderer;
    [SerializeField] private CanvasRenderer _targetRenderer;

    private Mesh _drawMesh;
    private Mesh _targetMesh;
    private Vector2 _lastMousePosition;

    private void Start()
    {
        CreateTargetLine();
    }

    private void Update()
    {
        StartNewLine();
        ContinueLine();
    }


    private void CreateTargetLine()
    {
        float borderSize = 500;

        Vector2 point = new Vector2(
            Random.Range(borderSize, 1920 - borderSize),
            Random.Range(borderSize, 1080 - borderSize));

        float currentAngle = Random.Range(0f, 360f);
        float rotationAmount = Random.Range(3f, 7f);
        float stepSize = Random.Range(10f, 20f);

        Vector2 drift = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));

        int numSteps = Random.Range(70, 150);

        _targetMesh = StartMesh(point, _targetMesh, _targetMaterial, _targetRenderer);
        for (int i = 0; i < numSteps; i++)
        {
            Vector2 previousPoint = point;
            point += Rotate(new Vector2(0f, stepSize), currentAngle) + drift;
            ContinueMesh(point, _targetMesh, _targetRenderer, previousPoint);

            Debug.Log(point);

            currentAngle += rotationAmount;
        }
    }

    private void StartNewLine()
    {
        if (!_leftMouse.action.WasPressedThisFrame()) return;

        Vector2 mousePosition = GetMousePosition();
        _lastMousePosition = mousePosition;

        _drawMesh = StartMesh(mousePosition, _drawMesh, _drawMaterial, _drawRenderer);
    }

    private void ContinueLine()
    {
        if (!_leftMouse.action.IsPressed()) return;

        Vector2 mousePosition = GetMousePosition();
        float distanceFromLastPoint = Vector2.Distance(mousePosition, _lastMousePosition);
        if (distanceFromLastPoint < LineVertexFrequency) return;

        ContinueMesh(mousePosition, _drawMesh, _drawRenderer, _lastMousePosition);

        _lastMousePosition = mousePosition;
    }

    private Mesh StartMesh(Vector2 point, Mesh lineMesh, Material material, CanvasRenderer renderer)
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(point.x, point.y);
        vertices[1] = new Vector3(point.x, point.y);
        vertices[2] = new Vector3(point.x, point.y);
        vertices[3] = new Vector3(point.x, point.y);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        lineMesh = new Mesh();
        lineMesh.MarkDynamic();

        lineMesh.vertices = vertices;
        lineMesh.uv = uvs;
        lineMesh.triangles = triangles;
        lineMesh.MarkDynamic();

        renderer.SetMesh(lineMesh);
        renderer.SetMaterial(material, null);

        return lineMesh;
    }

    private void ContinueMesh(Vector2 point, Mesh lineMesh, CanvasRenderer renderer, Vector2 lastPoint)
    {
        Vector3[] vertices = new Vector3[lineMesh.vertices.Length + 2];
        Vector2[] uvs = new Vector2[lineMesh.uv.Length + 2];
        int[] triangles = new int[lineMesh.triangles.Length + 6];

        lineMesh.vertices.CopyTo(vertices, 0);
        lineMesh.uv.CopyTo(uvs, 0);
        lineMesh.triangles.CopyTo(triangles, 0);

        int vIndex = vertices.Length - 4;

        Vector2 mouseForward = (point - lastPoint).normalized;
        Vector2 offsetVector = Rotate(mouseForward, 90f);
        Vector2 upVertex = point + (offsetVector * LineThickness);
        Vector2 downVertex = point - (offsetVector * LineThickness);

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

        lineMesh.vertices = vertices;
        lineMesh.uv = uvs;
        lineMesh.triangles = triangles;
        renderer.SetMesh(lineMesh);
    }

    private Vector2 Rotate(Vector2 vector, float angleDegrees)
    {
        Quaternion rotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);
        return rotation * vector;
    }

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = _mousePosition.action.ReadValue<Vector2>();
        mousePosition.x = (mousePosition.x / Screen.width) * 1920f;
        mousePosition.y = (mousePosition.y / Screen.width) * 1920f;
        
        return mousePosition;
    }
}
