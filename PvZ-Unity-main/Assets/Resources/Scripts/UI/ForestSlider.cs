using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestSlider : MonoBehaviour
{
    public Slider slider; // ��Unity�༭�������뻬�������
    public int maxValue = 100; // ����������ֵ
    public int initialValue = 30; // ��������ʼֵ
    public float smoothSpeed = 1.0f; // ���ƻ����ٶȣ�ֵԽ���ٶ�Խ��
    public GameObject goodLeafs;
    public GameObject goodLeafsSign;

    private Coroutine currentCoroutine; // ���浱ǰ�Ļ���Э��

    void Start()
    {
        if(GameManagement.levelData.levelEnviornment == "Forest")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        // ��ʼ��������
        slider.maxValue = maxValue;
        slider.value = 20;
    }

    /// <summary>
    /// ƽ�����û�������ֵ
    /// </summary>
    /// <param name="newValue">Ŀ��ֵ</param>
    public void SetSliderValueSmooth(int newValue)
    {
        if(gameObject.activeSelf)
        {
            // ȷ��Ŀ��ֵ�ںϷ���Χ��
            newValue = Mathf.Clamp(newValue, 0, maxValue);

            // ֹͣ��ǰ���е�Э�̣�������Э��ͬʱ���У�
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // �����µ�Э��ƽ���ƶ�������
            currentCoroutine = StartCoroutine(SmoothMoveSlider(newValue));
        }
        
    }

    /// <summary>
    /// ƽ���ƶ���������Э��
    /// </summary>
    /// <param name="targetValue">Ŀ��ֵ</param>
    /// <returns></returns>
    private IEnumerator SmoothMoveSlider(float targetValue)
    {
        if (gameObject.activeSelf)
        {
            float stopThreshold = 0.08f; // ������С������ֵ

            // ��������ָ������ֵʱ��������
            while (Mathf.Abs(slider.value - targetValue) > stopThreshold)
            {
                slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * smoothSpeed);
                yield return null; // �ȴ���һ֡
            }

            slider.value = targetValue; // ȷ�����վ�ȷ��Ŀ��ֵ
            if (slider.value == 0)
            {
                plantBuff();
                slider.value = initialValue;
            }
        }
            
    }

    public void plantBuff()
    {
        if (GameManagement.levelData.levelEnviornment == "Forest")
        {
            goodLeafs.SetActive(true);
            goodLeafsSign.SetActive(true);
        }
        
    }

    public void woodDreamBuff()
    {
            goodLeafs.SetActive(true);
            goodLeafsSign.SetActive(true);
        
    }


    /// <summary>
    /// ���ӻ�������ֵ,ʵ������ɭ��ֵ����ʱ
    /// </summary>
    /// <param name="increment">���ӵ���</param>
    public void IncreaseSliderValueSmooth(int increment)
    {
        if (gameObject.activeSelf)
        {
            SetSliderValueSmooth((int)slider.value + increment);
        }
            
    }

    /// <summary>
    /// ���ٻ�������ֵ��ʵ������ɭ��ֵ����ʱ
    /// </summary>
    /// <param name="decrement">���ٵ���</param>
    public void DecreaseSliderValueSmooth(int decrement)
    {
        if(gameObject.activeSelf)
        {
            decrement = (int)(decrement * 2 / GameManagement.GameDifficult);
            SetSliderValueSmooth((int)slider.value - decrement);
        }
        
    }
}
