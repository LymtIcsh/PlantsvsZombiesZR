using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // ��ת�ٶ�

    void Update()
    {
        // ��z��Ϊ������ת
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}

