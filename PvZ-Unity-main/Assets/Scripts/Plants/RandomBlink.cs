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
        // 如果 Inspector 没拖 Animator，就尝试自动获取
        if (animator == null)
            animator = GetComponent<Animator>();

        // 启动协程
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // 随机等待 1–3 秒
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // 触发 Blink
            animator.SetTrigger(triggerName);
        }
    }
}
