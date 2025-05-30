using UnityEngine;
using System.Collections;

public class ChargedObject : MonoBehaviour
{
    public GameObject objectToSpawn; // 要生成的物体
    const float chargeTime = 1f;   // 充能时间
    private bool isCharged = false;   // 是否充能完成
    private SpriteRenderer spriteRenderer; // SpriteRenderer 组件

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0); // 设置初始透明度为0
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        float elapsedTime = 0f;
        while (elapsedTime < chargeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / chargeTime); // 计算透明度
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha); // 设置透明度
            yield return null; // 等待下一帧
        }

        isCharged = true; // 充能完成
    }

    private void OnMouseDown()
    {
        if (isCharged)
        {
            GenerateObject(); // 生成物体
            isCharged = false; // 重置充能状态
            StartCoroutine(Charge()); // 重新充能
        }
    }

    private void GenerateObject()
    {
        // 在自身位置生成物体
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
