using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 场景间持续存在 : MonoBehaviour
{
    private static 场景间持续存在 instance;
    public TMP_Text 版本号;
    void Awake()
    {
        Application.targetFrameRate = 120;
        StaticThingsManagement.打开二级界面 = false;
        // 确保只有一个AudioManager实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持此物体在场景切换时不销毁
        }
        else
        {
            // 如果已有实例存在，销毁当前重复的AudioManager
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.visible = true;
        版本号.text = "v" + Application.version;
        SetAchievement.SetAchievementCompleted("百花齐放、百家争鸣");
        
    }

}
