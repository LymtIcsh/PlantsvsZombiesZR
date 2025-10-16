using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// 场景间持续存在
/// </summary>
public class PersistentAcrossScenes : MonoBehaviour
{
    private static PersistentAcrossScenes instance;
     [Header("版本号Text")]
    public TMP_Text versionNumberText;
    void Awake()
    {
        Application.targetFrameRate = 120;
        StaticThingsManagement.IsSecondaryPanelOpen = false;
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
        versionNumberText.text = "v" + Application.version;
        SetAchievement.SetAchievementCompleted("百花齐放、百家争鸣");
        
    }

}
