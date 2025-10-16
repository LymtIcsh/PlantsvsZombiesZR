using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(Camera))]
public class InfiniteFrustum : MonoBehaviour
{
    [Tooltip("一个很大的距离，用于扩展剔除范围")]
    public float extent = 1f;

    Camera _cam;

    void OnEnable()
    {
        _cam = GetComponent<Camera>();
        UpdateCullingMatrix();
    }

    void OnValidate()
    {
        if (_cam == null) _cam = GetComponent<Camera>();
        UpdateCullingMatrix();
    }

    void UpdateCullingMatrix()
    {
        float n = _cam.nearClipPlane;
        float f = Mathf.Max(extent, n + 0.01f);

        // 构造一个对称的、非常宽的视锥
        float left = -f;
        float right = f;
        float bottom = -f * _cam.aspect;
        float top = f * _cam.aspect;

        // 注意：GL.GetGPUProjectionMatrix 用于兼容不同平台的投影矩阵格式
        Matrix4x4 proj = Matrix4x4.Frustum(left, right, bottom, top, n, f);
        proj = GL.GetGPUProjectionMatrix(proj, false);

        // 把 cullingMatrix 设成 “超大投影矩阵 × 相机世界到相机矩阵”
        _cam.cullingMatrix = proj * _cam.worldToCameraMatrix;
    }
}
