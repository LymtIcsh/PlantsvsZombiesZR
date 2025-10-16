using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间
using UnityEngine.UI; // 引入UI命名空间

public class SceneSwitcher : MonoBehaviour
{
    // 这个函数可以被按钮的点击事件调用
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        // 加载新的场景
        SceneManager.LoadScene(sceneName);
    }
}

