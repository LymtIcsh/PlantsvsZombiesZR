using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    void Update()
    {
        // 检测 F 键的按下du
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 切换全屏和窗口化模式
            ToggleFullscreen();
        }
    }

    // 切换全屏模式和窗口模式
    void ToggleFullscreen()
    {
        // 判断当前是否全屏，如果是则切换为窗口模式，否则切换为全屏
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;  // 切换为窗口化
            Debug.Log("切换为窗口模式");
        }
        else
        {
            Screen.fullScreen = true;   // 切换为全屏
            Debug.Log("切换为全屏模式");
        }
    }
}
