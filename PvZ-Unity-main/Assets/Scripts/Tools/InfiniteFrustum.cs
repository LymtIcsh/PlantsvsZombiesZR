using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(Camera))]
public class InfiniteFrustum : MonoBehaviour
{
    [Tooltip("һ���ܴ�ľ��룬������չ�޳���Χ")]
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

        // ����һ���ԳƵġ��ǳ������׶
        float left = -f;
        float right = f;
        float bottom = -f * _cam.aspect;
        float top = f * _cam.aspect;

        // ע�⣺GL.GetGPUProjectionMatrix ���ڼ��ݲ�ͬƽ̨��ͶӰ�����ʽ
        Matrix4x4 proj = Matrix4x4.Frustum(left, right, bottom, top, n, f);
        proj = GL.GetGPUProjectionMatrix(proj, false);

        // �� cullingMatrix ��� ������ͶӰ���� �� ������絽�������
        _cam.cullingMatrix = proj * _cam.worldToCameraMatrix;
    }
}
