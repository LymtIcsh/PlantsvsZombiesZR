using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCoroutineHandler : MonoBehaviour
{

    

    public void StartAchievementCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);  // 启动传入的协程
    }


    private static AchievementCoroutineHandler _instance;

    public static AchievementCoroutineHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                // 如果单例还没创建，尝试查找它
                _instance = FindFirstObjectByType<AchievementCoroutineHandler>();
                if (_instance == null)
                {
                    // 如果场景中找不到，创建一个新的 GameObject 来挂载此脚本
                    GameObject obj = new GameObject("AchievementCoroutineHandler");
                    _instance = obj.AddComponent<AchievementCoroutineHandler>();
                }
            }
            return _instance;
        }
    }

    // 确保 Start 或 Awake 中的逻辑不会造成错误
    void Awake()
    {
        // 确保只有一个实例
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
