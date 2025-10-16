using UnityEngine;
using UnityEngine.UI;

public class GameDifficultyManagement : MonoBehaviour
{
    public Slider difficultySlider;  // 滑动条组件
    public Text difficultyText;  // 显示当前难度的文本（可选）

    private void Start()
    {
        // 初始化滑动条，并监听数值变化
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        // 设置滑动条的初始值为当前的GameDifficult
        difficultySlider.value = GameManagement.GameDifficult;
        UpdateDifficultyText();
    }

    // 当滑动条的值变化时调用此函数
    private void OnDifficultyChanged(float value)
    {
        GameManagement.GameDifficult = value;  // 将滑动条的值赋给GameDifficult
        UpdateDifficultyText();
    }

    // 更新UI上显示的当前难度值（可选）
    private void UpdateDifficultyText()
    {
    }
}

