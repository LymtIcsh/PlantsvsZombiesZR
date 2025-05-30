using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class SetAchievement : MonoBehaviour
{
    private static SetAchievement _instance;

    public static SetAchievement Instance
    {
        get
        {
            if (_instance == null)
            {
                // 如果单例还没创建，尝试查找它
                _instance = FindFirstObjectByType<SetAchievement>();
                if (_instance == null)
                {
                    // 如果场景中找不到，创建一个新的 GameObject 来挂载此脚本
                    GameObject obj = new GameObject("AchievementCoroutineHandler");
                    _instance = obj.AddComponent<SetAchievement>();
                }
            }
            return _instance;
        }
    }
    private static bool isProcessing = false; // 标记是否正在处理成就
    private static Queue<System.Action> achievementQueue = new Queue<System.Action>(); // 存储待执行的成就任务

    public static void SetAchievementCompleted(string achievementName)
    {

        // 将任务添加到队列中
        achievementQueue.Enqueue(() => CompleteAchievement(achievementName));

        // 如果当前没有正在处理任务，开始处理队列
        if (!isProcessing)
        {
            SetAchievement.Instance.StartCoroutine(ProcessQueue());
        }
    }


    // 处理队列中的任务
    private static IEnumerator ProcessQueue()
    {
        isProcessing = true;

        // 处理队列中的所有任务
        while (achievementQueue.Count > 0)
        {
            // 获取队列中的下一个任务
            System.Action currentTask = achievementQueue.Dequeue();

            // 执行任务
            currentTask.Invoke();

            // 等待 5 秒后再执行下一个任务
            yield return new WaitForSeconds(1.1f);
        }

        // 所有任务处理完毕
        isProcessing = false;
    }

    // 完成指定的成就
    private static void CompleteAchievement(string achievementName)
    {
        if (AchievementManager.achievements == null || AchievementManager.achievements.Length == 0)
        {
            Debug.LogError("AchievementManager.achievements is null or empty. Make sure it's initialized before use.");
            return;
        }


        Achievement achievement = System.Array.Find(AchievementManager.achievements, a => a.name == achievementName);

        if (achievement != null && !achievement.isCompleted)
        {
            achievement.isCompleted = true;

            Debug.Log($"成就 '{achievementName}' 已完成");

            // 创建或显示成就描述
            ShowAchievementDescription(achievementName);

            // 保存成就数据
            AchievementManager.SaveAchievements();
        }
    }



    // 设置所有成就为已完成
    public static void SetAllAchievementsCompleted()
    {
        // 确保成就列表存在且非空
        if (AchievementManager.achievements == null || AchievementManager.achievements.Length == 0)
        {
            Debug.LogError("AchievementManager.achievements is null or empty. Make sure it's initialized before use.");
            return;
        }

        foreach (var achievement in AchievementManager.achievements)
        {
            // 为每个成就调用完成方法
            SetAchievementCompleted(achievement.name);
        }

        Debug.Log("所有成就已完成");
        AchievementManager.SaveAchievements(); // 保存到 JSON 文件
    }


    // 设置所有成就为未完成
    public static void SetAllAchievementsNotCompleted()
    {
        foreach (var achievement in AchievementManager.achievements)
        {
            if (achievement.isCompleted)
            {
                achievement.isCompleted = false;
            }
        }

        Debug.Log("所有成就已重置为未完成");
        AchievementManager.SaveAchievements(); // 保存到 JSON 文件
    }



    public static void ShowAchievementDescription(string achievementName)
    {
        GameObject achievementObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ShowAchievement"));
        

        if (achievementObject != null)
        {
            // 激活物体
            achievementObject.SetActive(true);



            // 查找名为 "成就描述" 的子物体
            Transform descriptionTransform = FindChildByName(achievementObject.transform,"成就描述");

            if (descriptionTransform != null)
            {
                // 获取子物体中的 Text 组件
                Text descriptionText = descriptionTransform.GetComponent<Text>();

                if (descriptionText != null)
                {
                    // 设置成就描述文本为指定内容
                    descriptionText.text = achievementName;
                }
                else
                {
                    Debug.LogError("成就描述子物体没有找到 Text 组件");
                }
            }
            else
            {
                Debug.LogError("没有找到名为 '成就描述' 的子物体");
            }
        }
        else
        {
            Debug.LogError("没有找到名为 'ShowAchievement' 的物体");
        }
    }

    // 自定义方法来查找不活跃的物体
    private static GameObject FindInactiveObjectByName(string name)
    {
        // 遍历场景中的所有物体（包括不活跃的）
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    private static Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            // 递归查找子物体
            Transform result = FindChildByName(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
