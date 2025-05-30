using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SunBase : MonoBehaviour
{
    public int sunNumber;   //值多少阳光

    protected bool dropState;  //阳光是否正在掉落
    bool pickState;  //阳光是否被拾取
    Vector3 finalPos = new Vector3(-4.705f, 2.601f, 0f);  //拾取阳光动画终点
    float timer = 0, disappearTime = 15.0f;   //计时器，阳光多久后消失
    SpriteRenderer mySpriteRenderer;   //用于阳光逐渐消失

    //用于增加阳光
    SunNumber sunControl;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        dropState = true;
        pickState = false;
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sunControl = GameManagement.instance.SunText;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.CollectSun && dropState)
        {
            bePickedUp();
        }
            

        timer += Time.deltaTime;
        if (dropState == true) drop();
        if (pickState == true) collect();
        if (pickState == false && timer > disappearTime) disappear();
    }

    public abstract void drop();

    public void bePickedUp()
    {
        dropState = false;
        pickState = true;

        //播放音效
        AudioManager.Instance.PlaySoundEffect(15);
    }

    private void collect()
    {
        if (Vector3.Distance(transform.position, finalPos) > 0.1f)  // 不在终点，向终点移动
        {
            transform.Translate((finalPos - transform.position) * 4 * Time.deltaTime);
        }
        else   // 在终点，阳光数增加，启动缩放过程
        {
            StartCoroutine(FadeOut());  // 启动缩放协程
        }
    }

    private IEnumerator FadeOut()
    {
        Renderer renderer = GetComponent<SpriteRenderer>();  // 获取物体的Renderer组件
        Color initialColor = renderer.material.color;  // 记录初始颜色
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);  // 目标颜色透明度为0

        float duration = 0.2f;  // 渐变过程持续时间
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            renderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);  // 渐变颜色
            elapsedTime += Time.deltaTime;  // 计算经过的时间
            yield return null;  // 等待下一帧
        }

        renderer.material.color = targetColor;  // 确保最后的颜色为目标颜色
        sunControl.addSun(sunNumber);
        Destroy(gameObject);  // 销毁物体
    }


    private void disappear()
    {
        float alpha = 1 - (timer - disappearTime) * 3;
        if (alpha > 0) mySpriteRenderer.color = new Color(255, 255, 255, alpha);
        else Destroy(gameObject);
    }
}
