using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool isCompleted;
}

[System.Serializable]
public class AchievementList
{
    public Achievement[] achievements;
}

public class AchievementManager : MonoBehaviour
{
    public static Achievement[] achievements;
    private static string filePath;
    void Awake()
    {
        filePath = Application.persistentDataPath + "/achievements.json";  // 设置文件路径
        // 加载成就数据
        LoadAchievements();
    }

    

    // 静态方法，用于标记成就完成
    public static void CompleteAchievement(string achievementName)
    {
        // 找到对应的成就
        Achievement achievement = System.Array.Find(achievements, a => a.name == achievementName);

        if (achievement != null && !achievement.isCompleted)
        {
            // 设置为完成
            achievement.isCompleted = true;


            // 保存成就数据
            SaveAchievements();
        }
    }

    // 保存成就数据到 JSON 文件
    public static void SaveAchievements()
    {
        AchievementList achievementList = new AchievementList();
        achievementList.achievements = achievements;

        string json = JsonUtility.ToJson(achievementList, true);  // 转换为 JSON 字符串
        File.WriteAllText(filePath, json);  // 保存到文件
        AchievementUI achievementUI = FindFirstObjectByType<AchievementUI>();
        if(achievementUI != null)
        {
            achievementUI.GenerateAchievements();
        }
    }

    // 从 JSON 文件加载成就数据
    public static void LoadAchievements()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);  // 读取文件内容
            AchievementList achievementList = JsonUtility.FromJson<AchievementList>(json);  // 解析 JSON

            achievements = achievementList.achievements;

            // 检查是否有新增成就，若有则添加到当前成就数组
            CheckAndAddMissingAchievements();
        }
        else
        {
            // 如果文件不存在，初始化默认成就
            achievements = new Achievement[]
            {
                new Achievement { name = "百花齐放、百家争鸣", description = "进入游戏", isCompleted = false },
                new Achievement { name = "梦开始的地方！", description = "进入关卡1 - 1", isCompleted = false },
                //new Achievement { name = "爱玩玩不玩滚，喵", description = "点击退出按钮", isCompleted = false },
                //new Achievement { name = "谢谢", description = "查看制作者名单", isCompleted = false },
                new Achievement { name = "这植物怎么玩啊？", description = "查看植物图鉴", isCompleted = false },
                new Achievement { name = "9990", description = "单局游戏积累超过9990点阳光", isCompleted = false },
                new Achievement { name = "时轮逆旅（TRJ）", description = "调整时间流速", isCompleted = false },
                new Achievement { name = "如听仙乐耳暂明", description = "使用唱片", isCompleted = false },
                //new Achievement { name = "最终，森林会记住一切", description = "查看森林记忆", isCompleted = false },
                //new Achievement { name = "森林记忆·结局？", description = "击败僵尸博士-森林", isCompleted = false },
                new Achievement { name = "我不是毒神", description = "为单个僵尸附加超过60层毒素", isCompleted = false },
                //new Achievement { name = "老 玩 家", description = "游玩1.0正式版及以前版本的游戏", isCompleted = false },
                //new Achievement { name = "我是幸运礼盒！我才是幸运礼盒！", description = "用幸运礼盒开出幸运礼盒", isCompleted = false },
                new Achievement { name = "植物大战僵尸退化版", description = "让冰封向日葵变成向日葵", isCompleted = false },
                //new Achievement { name = "我到底是不是树啊！", description = "让智慧树说话", isCompleted = false },
                //new Achievement { name = "燃起来了！", description = "让森林纵火犯烧掉100个草丛", isCompleted = false },
                //new Achievement { name = "树桩终结者", description = "损坏50个森林树桩", isCompleted = false },
                //new Achievement { name = "艺术就是爆炸！", description = "引爆森林炸弹", isCompleted = false },
                //new Achievement { name = "我看到了「生生不息」的激荡！", description = "触发森林加成：森林的土壤为你孕育生生不息的芬芳！", isCompleted = false },
                //new Achievement { name = "师夷长技以制夷", description = "反弹一次植物僵尸的子弹。", isCompleted = false },
                //new Achievement { name = "回收再利用", description = "使用发光吸金磁吸取僵尸的防具", isCompleted = false },
            };

            // 创建一个新的 JSON 文件
            SaveAchievements();
        }
    }

    // 检查并添加缺少的成就
    private static void CheckAndAddMissingAchievements()
    {
        // 确定所有成就的列表（可以从代码中或者其他来源动态获取）
        Achievement[] defaultAchievements = new Achievement[]
        {
           new Achievement { name = "百花齐放、百家争鸣", description = "进入游戏", isCompleted = false },
                new Achievement { name = "梦开始的地方！", description = "进入关卡1 - 1", isCompleted = false },
                //new Achievement { name = "爱玩玩不玩滚，喵", description = "点击退出按钮", isCompleted = false },
                //new Achievement { name = "谢谢", description = "查看制作者名单", isCompleted = false },
                new Achievement { name = "这植物怎么玩啊？", description = "查看植物图鉴", isCompleted = false },
                new Achievement { name = "9990", description = "单局游戏积累超过9990点阳光", isCompleted = false },
                new Achievement { name = "时轮逆旅（TRJ）", description = "调整时间流速", isCompleted = false },
                new Achievement { name = "如听仙乐耳暂明", description = "使用唱片", isCompleted = false },
                //new Achievement { name = "最终，森林会记住一切", description = "查看森林记忆", isCompleted = false },
                //new Achievement { name = "森林记忆·结局？", description = "击败僵尸博士-森林", isCompleted = false },
                new Achievement { name = "我不是毒神", description = "为单个僵尸附加超过60层毒素", isCompleted = false },
                //new Achievement { name = "老 玩 家", description = "游玩1.0正式版及以前版本的游戏", isCompleted = false },
                //new Achievement { name = "我是幸运礼盒！我才是幸运礼盒！", description = "用幸运礼盒开出幸运礼盒", isCompleted = false },
                new Achievement { name = "植物大战僵尸退化版", description = "让冰封向日葵变成向日葵", isCompleted = false },
                //new Achievement { name = "我到底是不是树啊！", description = "让智慧树说话", isCompleted = false },
                //new Achievement { name = "燃起来了！", description = "让森林纵火犯烧掉100个草丛", isCompleted = false },
                //new Achievement { name = "树桩终结者", description = "损坏50个森林树桩", isCompleted = false },
                //new Achievement { name = "艺术就是爆炸！", description = "引爆森林炸弹", isCompleted = false },
                //new Achievement { name = "我看到了「生生不息」的激荡！", description = "触发森林加成：森林的土壤为你孕育生生不息的芬芳！", isCompleted = false },
                //new Achievement { name = "师夷长技以制夷", description = "反弹一次植物僵尸的子弹。", isCompleted = false },
                //new Achievement { name = "回收再利用", description = "使用发光吸金磁吸取僵尸的防具", isCompleted = false },
        };

        // 遍历默认成就，检查当前成就中是否包含该成就，若没有则添加
        foreach (var defaultAchievement in defaultAchievements)
        {
            if (Array.Find(achievements, a => a.name == defaultAchievement.name) == null)
            {
                // 将缺少的成就添加到数组
                Array.Resize(ref achievements, achievements.Length + 1);
                achievements[achievements.Length - 1] = defaultAchievement;
            }
        }

        // 如果添加了新的成就，保存更新后的数据
        SaveAchievements();
    }
}
