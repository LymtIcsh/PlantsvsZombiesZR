using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间以便使用Button组件

public class ButtonSetLevel : MonoBehaviour
{
    public int Buttonlevel; // 当前按钮对应的关卡数
    public bool isInitialLevel; // 是否为初始关卡
    private Button button; // 按钮组件引用

    void Start()
    {
        button = GetComponent<Button>(); // 获取当前GameObject的Button组件
        UpdateButtonVisibility(); // 初始化时更新按钮的可见性
    }

    public void SetLevel()
    {
        BeginManagement.level = Buttonlevel;
    }

    // 更新按钮的可见性
    public void UpdateButtonVisibility()
    {
        // 如果是初始关卡，无论前一个关卡是否通关都显示按钮
        if (isInitialLevel)
        {
            button.gameObject.SetActive(true);
            return; // 退出方法
        }

        // 检测当前按钮的关卡数-1
        int previousLevel = Buttonlevel - 1;

        // 检查前一个关卡是否通关
        if (LevelManagerStatic.IsLevelCompleted(previousLevel))
        {
            button.gameObject.SetActive(true); // 显示按钮
        }
        else
        {
            button.gameObject.SetActive(false); // 隐藏按钮
        }
    }
}
