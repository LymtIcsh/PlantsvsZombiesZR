using UnityEngine;
using UnityEngine.UI;

public class ResolutionSwitcher : MonoBehaviour
{
    private Vector2Int[] resolutions = new Vector2Int[]
    {
    new Vector2Int(7680, 4320),  // 16:9 8K 分辨率
    new Vector2Int(5120, 2880),  // 16:9 5K 分辨率
    new Vector2Int(3840, 2160),  // 16:9 4K 分辨率
    new Vector2Int(3200, 1800),  // 16:9 分辨率 (较高分辨率)
    new Vector2Int(2560, 1440),  // 16:9 QHD 分辨率 (2K)
    new Vector2Int(2560, 1080),  // 16:9 超宽分辨率
    new Vector2Int(2048, 1152),  // 16:9 中等分辨率
    new Vector2Int(1920, 1080),  // 16:9 Full HD 分辨率
    new Vector2Int(1600, 900),   // 16:9 HD+ 分辨率
    new Vector2Int(1440, 810),   // 16:9 HD++ 分辨率
    new Vector2Int(1366, 768),   // 16:9 HD 分辨率
    new Vector2Int(1280, 720),   // 16:9 HD 分辨率
    new Vector2Int(1024, 576),   // 16:9 较低分辨率
    new Vector2Int(960, 540),    // 16:9 较低分辨率
    };

    private int currentResolutionIndex = 2; // 当前选择的分辨率索引

    // UI 组件
    public Text resolutionText;   // 用于显示当前分辨率的文本
    public Button resolutionButton; // 按钮组件

    public Text buttonText; // 按钮文本组件

    void Start()
    {
        if(true)
        {
            // 获取当前屏幕分辨率
            Vector2Int currentResolution = new Vector2Int(Screen.width, Screen.height);

            // 查找当前分辨率在预设分辨率数组中的索引
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i] == currentResolution)
                {
                    currentResolutionIndex = i;
                    break;
                }
            }

            // 更新文本显示为当前分辨率
            UpdateResolutionText();
        }
    }

    // 按钮点击事件方法
    public void ChangeResolution()
    {
        // 切换到下一个分辨率
        currentResolutionIndex = (currentResolutionIndex + 1) % resolutions.Length;

        // 设置新的分辨率
        SetResolution(currentResolutionIndex);

        // 更新文本显示
        UpdateResolutionText();
    }

    // 设置分辨率的方法
    private void SetResolution(int index)
    {
        Vector2Int newResolution = resolutions[index];
        Screen.SetResolution(newResolution.x, newResolution.y, Screen.fullScreen);
    }

    // 更新文本显示的方法
    private void UpdateResolutionText()
    {
        Vector2Int currentResolution = resolutions[currentResolutionIndex];
        resolutionText.text = $"当前分辨率: {currentResolution.x}x{currentResolution.y}";
    }
}
