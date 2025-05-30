using UnityEngine;
using System.Collections;

public class ChargedObject : MonoBehaviour
{
    public GameObject objectToSpawn; // Ҫ���ɵ�����
    const float chargeTime = 1f;   // ����ʱ��
    private bool isCharged = false;   // �Ƿ�������
    private SpriteRenderer spriteRenderer; // SpriteRenderer ���

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��ȡ SpriteRenderer ���
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0); // ���ó�ʼ͸����Ϊ0
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        float elapsedTime = 0f;
        while (elapsedTime < chargeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / chargeTime); // ����͸����
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha); // ����͸����
            yield return null; // �ȴ���һ֡
        }

        isCharged = true; // �������
    }

    private void OnMouseDown()
    {
        if (isCharged)
        {
            GenerateObject(); // ��������
            isCharged = false; // ���ó���״̬
            StartCoroutine(Charge()); // ���³���
        }
    }

    private void GenerateObject()
    {
        // ������λ����������
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
