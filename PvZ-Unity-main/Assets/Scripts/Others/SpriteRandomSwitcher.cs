using UnityEngine;
using System.Collections;

public class SpriteColorScaleRandomSwitcher : MonoBehaviour
{
    public Sprite[] sprites; // �洢��� Sprite
    private SpriteRenderer spriteRenderer; // ���ڿ��� SpriteRenderer

    void Start()
    {
        // ��ȡ��ǰ����� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��ʼ�л� Sprite����ɫ�����ŵ�Э��
        StartCoroutine(SwitchSpriteColorAndScale());
    }

    private IEnumerator SwitchSpriteColorAndScale()
    {
        while (true) // ����ѭ����ֱ���ֶ�ֹͣЭ��
        {
            // ���ѡ��һ�� Sprite
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];

            // ���ѡ��һ��Ŀ����ɫ����ɫ���ɫ��
            Color targetColor = Random.value > 0.5f ? Color.red : Color.white;

            // ���ѡ��һ��Ŀ�����ţ�1.3 �� 1.5��
            float targetScaleX = Random.Range(1.3f, 1.5f);

            // ͬʱ������ɫ������
            yield return StartCoroutine(GraduallyChangeColorAndScale(targetColor, targetScaleX));

            // �ȴ� 0.3 �������л�
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator GraduallyChangeColorAndScale(Color targetColor, float targetScaleX)
    {
        float duration = 0.2f; // �������ʱ��
        float time = 0f;

        // ��ȡ��ǰ��ɫ������
        Color startColor = spriteRenderer.color;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(targetScaleX, startScale.y, startScale.z);

        while (time < duration)
        {
            time += Time.deltaTime;

            // ƽ��������ɫ
            spriteRenderer.color = Color.Lerp(startColor, targetColor, time / duration);

            // ƽ����������
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);

            yield return null; // �ȴ���һ֡
        }

        // ȷ��������ɫ������ֵ��ȷ��Ŀ��ֵ
        spriteRenderer.color = targetColor;
        transform.localScale = targetScale;
    }
}
