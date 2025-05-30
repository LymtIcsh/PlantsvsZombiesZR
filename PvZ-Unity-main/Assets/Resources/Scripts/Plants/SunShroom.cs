using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SunShroom : Plant
{
    public GameObject SmallSun;
    public GameObject BigSun;


    protected bool grew;
    protected Transform sunManagement;   //̫������������Tranform�����Ϊ����̫��������
    float createSunSpeed = 24f;

    protected override void Start()
    {
        base.Start();

        if (sunManagement != null)
        {
            sunManagement = GameManagement.instance.sunManagement.GetComponent<Transform>();
        }


        Invoke("createToTruth", 5);
        Invoke("grow", 80f);
    }

    private void grow()
    {
        AudioManager.Instance.PlaySoundEffect(53);
        GetComponent<Animator>().SetBool("grow", true);
        grew = true;
    }
    private void createToTruth()//�����л�����״̬
    {
        StartCoroutine(ColorSequence());
    }

    public virtual  void createSun()
    {
        if (grew)
        {
            //����̫��
            Instantiate(BigSun, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
        }
        else
        {
            Instantiate(SmallSun, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
        }
      
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
        // ===== 1. ׼���׶� =====
        // Ư���ƣ�#FFFF00��͸���� 93/255
        float fadeDuration = 1f;
        Color targetColor = new Color(1f, 1f, 0f, 93f / 255f);

        int len = allRenderers.Length;
        // ��������ʵ������¼ԭʼ��ɫ���ٶ���ʼ���ǰ��� alpha=0��
        Material[] mats = new Material[len];
        Color[] originalColors = new Color[len];
        for (int i = 0; i < len; i++)
        {
            mats[i] = allRenderers[i].material;
            originalColors[i] = mats[i].GetColor("_Color");
        }

        // ===== 2. ���䵽��ɫ��͸�� =====
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
        // ȷ����ȷ��Ŀ��״̬
        for (int i = 0; i < len; i++)
            mats[i].SetColor("_Color", targetColor);

        createSun();

        // ===== 4. �ָ���ԭʼ��ɫ =====
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
        // ���ջָ�
        for (int i = 0; i < len; i++)
            mats[i].SetColor("_Color", originalColors[i]);
    }
}
