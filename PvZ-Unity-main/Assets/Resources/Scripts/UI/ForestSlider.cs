using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestSlider : MonoBehaviour
{
    public Slider slider; // 在Unity编辑器中拖入滑动条组件
    public int maxValue = 100; // 滑动条上限值
    public int initialValue = 30; // 滑动条初始值
    public float smoothSpeed = 1.0f; // 控制滑动速度，值越大速度越快
    public GameObject goodLeafs;
    public GameObject goodLeafsSign;

    private Coroutine currentCoroutine; // 保存当前的滑动协程

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
        // 初始化滑动条
        slider.maxValue = maxValue;
        slider.value = 20;
    }

    /// <summary>
    /// 平滑设置滑动条的值
    /// </summary>
    /// <param name="newValue">目标值</param>
    public void SetSliderValueSmooth(int newValue)
    {
        if(gameObject.activeSelf)
        {
            // 确保目标值在合法范围内
            newValue = Mathf.Clamp(newValue, 0, maxValue);

            // 停止当前运行的协程（避免多个协程同时运行）
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // 启动新的协程平滑移动滑动条
            currentCoroutine = StartCoroutine(SmoothMoveSlider(newValue));
        }
        
    }

    /// <summary>
    /// 平滑移动滑动条的协程
    /// </summary>
    /// <param name="targetValue">目标值</param>
    /// <returns></returns>
    private IEnumerator SmoothMoveSlider(float targetValue)
    {
        if (gameObject.activeSelf)
        {
            float stopThreshold = 0.08f; // 增大最小差距的阈值

            // 当差距大于指定的阈值时继续滑动
            while (Mathf.Abs(slider.value - targetValue) > stopThreshold)
            {
                slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * smoothSpeed);
                yield return null; // 等待下一帧
            }

            slider.value = targetValue; // 确保最终精确到目标值
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
    /// 增加滑动条的值,实际用作森林值减少时
    /// </summary>
    /// <param name="increment">增加的量</param>
    public void IncreaseSliderValueSmooth(int increment)
    {
        if (gameObject.activeSelf)
        {
            SetSliderValueSmooth((int)slider.value + increment);
        }
            
    }

    /// <summary>
    /// 减少滑动条的值，实际用作森林值增加时
    /// </summary>
    /// <param name="decrement">减少的量</param>
    public void DecreaseSliderValueSmooth(int decrement)
    {
        if(gameObject.activeSelf)
        {
            decrement = (int)(decrement * 2 / GameManagement.GameDifficult);
            SetSliderValueSmooth((int)slider.value - decrement);
        }
        
    }
}
