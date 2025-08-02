using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    public const float SECONDS_PER_FRAME = .5f;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Texture _frame0;
    [SerializeField] private Texture _frame1;

    private float _animTime;

    private void Update()
    {
        _animTime += Time.deltaTime / SECONDS_PER_FRAME;
        int frame = Mathf.FloorToInt(_animTime) % 2;

        _meshRenderer.material.SetTexture("_BaseMap", frame == 0 ? _frame0 : _frame1);
    }
}
