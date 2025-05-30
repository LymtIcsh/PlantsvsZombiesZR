using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // 旋转速度

    void Update()
    {
        // 以z轴为中心旋转
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}

