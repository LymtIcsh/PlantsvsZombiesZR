using System.Collections;
using UnityEngine;

public class RandomBlink : MonoBehaviour
{
    Animator animator;

    string triggerName = "Blink";

    float minInterval = 5f;
    float maxInterval = 10f;

    void Start()
    {
        // ��� Inspector û�� Animator���ͳ����Զ���ȡ
        if (animator == null)
            animator = GetComponent<Animator>();

        // ����Э��
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // ����ȴ� 1�C3 ��
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // ���� Blink
            animator.SetTrigger(triggerName);
        }
    }
}
