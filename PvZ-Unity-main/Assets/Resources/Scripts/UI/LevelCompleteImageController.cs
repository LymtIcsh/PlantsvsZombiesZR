using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI; // 需要引用 UI

public class LevelCompleteImageController : MonoBehaviour
{
    public int levelToCheck; // 要检查的关卡号
    private Image levelCompleteImage;
    public bool mustShow;//开启后会固定显示植物，无论关卡是否通关

    void Start()
    {
        // 获取当前物体上的 Image 组件
        levelCompleteImage = GetComponent<Image>();

        // 检查关卡是否通关
        CheckLevelCompletion();
    }

    void CheckLevelCompletion()
    {
        // 调用 LevelManager 或静态类的方法来检查该关卡是否已通关
        if (LevelManagerStatic.IsLevelCompleted(levelToCheck))
        {
            // 如果通关，显示图像
            levelCompleteImage.enabled = true;
            
            
        }
        else
        {
            // 否则隐藏图像
            levelCompleteImage.enabled = false;
            
        }
        if(mustShow)
        {

            levelCompleteImage.enabled = true;
        }
    }
}
