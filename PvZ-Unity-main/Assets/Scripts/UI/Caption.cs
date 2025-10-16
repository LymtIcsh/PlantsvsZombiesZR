using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caption : MonoBehaviour
{
    Queue<CaptionNode> captionQueue = new Queue<CaptionNode>();   // ��չʾ��Ļ����
    SpriteRenderer spriteRenderer;   // ����� SpriteRenderer ���

    // ���Ŷ�������
    Vector3 minScale = new Vector3(180f, 180f, 1f);   // ��ʼʱ����С scale
    Vector3 maxScale = new Vector3(190f, 190f, 1f);   // ����ʱ����� scale
    float scaleTimer = 0f;      // ���ż�ʱ��

    // չʾ����
    bool isShowing = false;   // �Ƿ�����չʾ��Ļ
    CaptionNode nowNode;      // ��ǰ�ڵ�

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

    // ���Ų���ʱ����������
    private void updateCaption()
    {
        scaleTimer += Time.deltaTime;
        // �����ֵ����
        float t = Mathf.Clamp01(scaleTimer / nowNode.showTime);
        transform.localScale = Vector3.Lerp(minScale, maxScale, t);

        if (scaleTimer >= nowNode.showTime)
        {
            // ʱ�䵽������չʾ
            isShowing = false;
            spriteRenderer.enabled = false;
        }
    }

    // �л�����һ����Ļ
    private void changeCaption()
    {
        nowNode = captionQueue.Dequeue();

        // ������ͼ������״̬
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/UI/Caption/" + nowNode.caption);
        transform.localScale = minScale;
        spriteRenderer.enabled = true;

        scaleTimer = 0f;
        isShowing = true;

        AudioManager.Instance.PlaySoundEffectByName(nowNode.caption);
    }

    // --- ���²��䣬ֻ��չʾ��ͬ�Ķ������� ---
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
