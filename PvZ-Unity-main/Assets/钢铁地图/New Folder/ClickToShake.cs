using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;        // ��������ʱ��
    public float shakeMagnitude = 0.1f;       // ��������
    public LayerMask targetLayer;             // Ҫ��Ӧ�����ͼ��

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
            // ����Ƿ����� UI ��
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ��ͼ����е�����
            Collider2D hit = Physics2D.OverlapPoint(mousePos, targetLayer);
            if (hit != null && hit.gameObject == gameObject)
            {
                StartShake();
            }
        }

        // �����߼�
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
