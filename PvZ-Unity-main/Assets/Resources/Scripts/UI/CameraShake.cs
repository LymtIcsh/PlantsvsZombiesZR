using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ����ģʽ�����������ű�����
    public static CameraShake Instance;

    private Vector3 _originalPosition; // ���ԭʼλ��
    private bool _isShaking = false;   // �Ƿ����ڶ���

    private void Awake()
    {
        // ������ʼ��
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �ⲿ���õĶ�������
    public void Shake(float duration, float magnitude)
    {
        if (!_isShaking)
        {
            _originalPosition = transform.localPosition; // ��¼���ԭʼλ��
            StartCoroutine(DoShake(duration, magnitude));
        }
    }

    // ����Э��,����ʱ��+ǿ��
    private System.Collections.IEnumerator DoShake(float duration, float magnitude)
    {
        _isShaking = true;

        float elapsed = 0f; // �ѹ�ȥ��ʱ��

        while (elapsed < duration)
        {
            // �������ƫ����
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Ӧ��ƫ���������λ��
            transform.localPosition = _originalPosition + new Vector3(x, y, 0);

            // ����ʱ��
            elapsed += Time.deltaTime;

            yield return null; // �ȴ���һ֡
        }

        // �����������ָ����ԭʼλ��
        transform.localPosition = _originalPosition;
        _isShaking = false;
    }
}