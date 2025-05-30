using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;        // 抖动持续时间
    public float shakeMagnitude = 0.1f;       // 抖动幅度
    public LayerMask targetLayer;             // 要响应点击的图层

    private Vector3 originalPosition;
    private float elapsedTime = 0f;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 检查是否点击在 UI 上
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 用图层进行点击检测
            Collider2D hit = Physics2D.OverlapPoint(mousePos, targetLayer);
            if (hit != null && hit.gameObject == gameObject)
            {
                StartShake();
            }
        }

        // 抖动逻辑
        if (isShaking)
        {
            if (elapsedTime < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeMagnitude;
                float y = Random.Range(-1f, 1f) * shakeMagnitude;
                transform.position = originalPosition + new Vector3(x, y, 0f);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                isShaking = false;
                transform.position = originalPosition;
            }
        }
    }

    void StartShake()
    {
        originalPosition = transform.position;
        elapsedTime = 0f;
        isShaking = true;
    }
}
