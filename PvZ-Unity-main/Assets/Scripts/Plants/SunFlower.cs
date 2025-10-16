using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    public GameObject flowersunPrefab;   //向日葵太阳预制体

    protected Transform sunManagement;   //太阳管理器对象Tranform组件，为所有太阳父对象
    float createSunSpeed = 24f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GameObject sunManage = GameManagement.instance.sunManagement;

        if (sunManage != null )
        {
            sunManagement = sunManage.GetComponent<Transform>();
        }
        

        Invoke("createToTruth", 5);
    }


    private void createToTruth()//用于切换动画状态
    {
        //gameObject.GetComponent<Animator>().SetBool("create", true);
        StartCoroutine(ColorSequence());
    }

    public virtual void createSun()
    {
        //生成太阳
        Instantiate(flowersunPrefab, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);

        Invoke("createToTruth", createSunSpeed);
    }


    public override void cold()
    {
        base.cold();
        createSunSpeed = 48f;
    }

    public override void warm()
    {
        base.warm();
        createSunSpeed = 24f;
    }

    public override void normal()
    {
        base.normal();
        createSunSpeed = 24f;
    }

    protected override void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
        createSunSpeed = 16f;
    }

    protected override void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
        createSunSpeed = 24f;
    }

    private IEnumerator ColorSequence()
    {
        // ===== 1. 准备阶段 =====
        // 漂亮黄：#FFFF00，透明度 93/255
        float fadeDuration = 1f;
        Color targetColor = new Color(1f, 1f, 0f, 93f / 255f);

        int len = allRenderers.Length;
        // 拷贝材质实例并记录原始颜色（假定初始都是白且 alpha=0）
        Material[] mats = new Material[len];
        Color[] originalColors = new Color[len];
        for (int i = 0; i < len; i++)
        {
            mats[i] = allRenderers[i].material;
            originalColors[i] = mats[i].GetColor("_Color");
        }

        // ===== 2. 渐变到黄色半透明 =====
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float frac = Mathf.Clamp01(t / fadeDuration);
            for (int i = 0; i < len; i++)
            {
                Color c = Color.Lerp(originalColors[i], targetColor, frac);
                mats[i].SetColor("_Color", c);
            }
            yield return null;
        }
        // 确保精确到目标状态
        for (int i = 0; i < len; i++)
            mats[i].SetColor("_Color", targetColor);

        createSun();

        // ===== 4. 恢复到原始颜色 =====
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float frac = Mathf.Clamp01(t / fadeDuration);
            for (int i = 0; i < len; i++)
            {
                Color c = Color.Lerp(targetColor, originalColors[i], frac);
                mats[i].SetColor("_Color", c);
            }
            yield return null;
        }
        // 最终恢复
        for (int i = 0; i < len; i++)
            mats[i].SetColor("_Color", originalColors[i]);
    }
}
