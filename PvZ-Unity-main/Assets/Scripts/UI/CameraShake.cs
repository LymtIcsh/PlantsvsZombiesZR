using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 单例模式，方便其他脚本调用
    public static CameraShake Instance;

    private Vector3 _originalPosition; // 相机原始位置
    private bool _isShaking = false;   // 是否正在抖动

    private void Awake()
    {
        // 单例初始化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 外部调用的抖动方法
    public void Shake(float duration, float magnitude)
    {
        if (!_isShaking)
        {
            _originalPosition = transform.localPosition; // 记录相机原始位置
            StartCoroutine(DoShake(duration, magnitude));
        }
    }

    // 抖动协程,持续时长+强度
    private System.Collections.IEnumerator DoShake(float duration, float magnitude)
    {
        _isShaking = true;

        float elapsed = 0f; // 已过去的时间

        while (elapsed < duration)
        {
            // 随机生成偏移量
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // 应用偏移量到相机位置
            transform.localPosition = _originalPosition + new Vector3(x, y, 0);

            // 更新时间
            elapsed += Time.deltaTime;

            yield return null; // 等待下一帧
        }

        // 抖动结束，恢复相机原始位置
        transform.localPosition = _originalPosition;
        _isShaking = false;
    }
}