using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caption : MonoBehaviour
{
    Queue<CaptionNode> captionQueue = new Queue<CaptionNode>();   // 待展示字幕队列
    SpriteRenderer spriteRenderer;   // 自身的 SpriteRenderer 组件

    // 缩放动画参数
    Vector3 minScale = new Vector3(180f, 180f, 1f);   // 开始时的最小 scale
    Vector3 maxScale = new Vector3(190f, 190f, 1f);   // 结束时的最大 scale
    float scaleTimer = 0f;      // 缩放计时器

    // 展示控制
    bool isShowing = false;   // 是否正在展示字幕
    CaptionNode nowNode;      // 当前节点

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("activate", 0.5f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        showGameStart();
    }

    void Update()
    {
        if (isShowing)
        {
            updateCaption();
        }
        else if (captionQueue.Count > 0)
        {
            changeCaption();
        }
    }

    private void activate()
    {
        gameObject.SetActive(true);
    }

    // 缩放并在时长到后隐藏
    private void updateCaption()
    {
        scaleTimer += Time.deltaTime;
        // 计算插值进度
        float t = Mathf.Clamp01(scaleTimer / nowNode.showTime);
        transform.localScale = Vector3.Lerp(minScale, maxScale, t);

        if (scaleTimer >= nowNode.showTime)
        {
            // 时间到，结束展示
            isShowing = false;
            spriteRenderer.enabled = false;
        }
    }

    // 切换到下一个字幕
    private void changeCaption()
    {
        nowNode = captionQueue.Dequeue();

        // 加载新图并重置状态
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/UI/Caption/" + nowNode.caption);
        transform.localScale = minScale;
        spriteRenderer.enabled = true;

        scaleTimer = 0f;
        isShowing = true;

        AudioManager.Instance.PlaySoundEffectByName(nowNode.caption);
    }

    // --- 以下不变，只是展示不同的队列内容 ---
    public void showGameStart()
    {
        captionQueue.Enqueue(new CaptionNode("StartReady", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartSet", 0.5f));
        captionQueue.Enqueue(new CaptionNode("StartPlant", 0.75f));
    }

    public void showWave()
    {
        captionQueue.Enqueue(new CaptionNode("HugeWave", 3f));
    }

    public void showFinalWave()
    {
        captionQueue.Enqueue(new CaptionNode("HugeWave", 3f));
        captionQueue.Enqueue(new CaptionNode("FinalWave", 3f));
    }
}

public class CaptionNode
{
    public string caption;
    public float showTime;
    public CaptionNode(string caption, float showTime)
    {
        this.caption = caption;
        this.showTime = showTime;
    }
}
