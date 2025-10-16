using UnityEngine;
using System.Collections;

public class SpriteColorScaleRandomSwitcher : MonoBehaviour
{
    public Sprite[] sprites; // 存储多个 Sprite
    private SpriteRenderer spriteRenderer; // 用于控制 SpriteRenderer

    void Start()
    {
        // 获取当前物体的 SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 开始切换 Sprite、颜色和缩放的协程
        StartCoroutine(SwitchSpriteColorAndScale());
    }

    private IEnumerator SwitchSpriteColorAndScale()
    {
        while (true) // 无限循环，直到手动停止协程
        {
            // 随机选择一个 Sprite
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];

            // 随机选择一个目标颜色（红色或白色）
            Color targetColor = Random.value > 0.5f ? Color.red : Color.white;

            // 随机选择一个目标缩放（1.3 到 1.5）
            float targetScaleX = Random.Range(1.3f, 1.5f);

            // 同时渐变颜色和缩放
            yield return StartCoroutine(GraduallyChangeColorAndScale(targetColor, targetScaleX));

            // 等待 0.3 秒后继续切换
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator GraduallyChangeColorAndScale(Color targetColor, float targetScaleX)
    {
        float duration = 0.2f; // 渐变持续时间
        float time = 0f;

        // 获取当前颜色和缩放
        Color startColor = spriteRenderer.color;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(targetScaleX, startScale.y, startScale.z);

        while (time < duration)
        {
            time += Time.deltaTime;

            // 平滑过渡颜色
            spriteRenderer.color = Color.Lerp(startColor, targetColor, time / duration);

            // 平滑过渡缩放
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);

            yield return null; // 等待下一帧
        }

        // 确保最终颜色和缩放值精确到目标值
        spriteRenderer.color = targetColor;
        transform.localScale = targetScale;
    }
}
