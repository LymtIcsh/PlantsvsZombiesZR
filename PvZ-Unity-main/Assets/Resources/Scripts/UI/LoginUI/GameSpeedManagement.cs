using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManagement : MonoBehaviour
{
    public Slider speedSlider; // 拖拽引用滑动条
    public Button Button;
    private float defaultTimeScale = 1f;
    private bool IsTimeout = false;

    void Start()
    {
        // 将滑动条的初始值设置为当前游戏速度
        speedSlider.value = defaultTimeScale;

        // 添加监听器，随时监控滑动条的值变化
        speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        Button.onClick.AddListener(OnTimeout);
    }

    // 当滑动条的值发生变化时调用该方法
    void OnSpeedSliderValueChanged(float value)
    {
        // 设置游戏的时间缩放（改变游戏速度）
        Time.timeScale = value;
    }

    void OnTimeout()
    {
        if (IsTimeout)
        {
            Time.timeScale = speedSlider.value;
            IsTimeout = false;
        }
        else
        {
            Time.timeScale = 0.1f;
            IsTimeout = true;
        }
    }

    void OnDestroy()
    {
        // 移除监听器（可选）
        speedSlider.onValueChanged.RemoveListener(OnSpeedSliderValueChanged);
    }
}
