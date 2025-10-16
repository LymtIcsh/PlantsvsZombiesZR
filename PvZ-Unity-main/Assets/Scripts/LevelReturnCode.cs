using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡返回代码 - 管理游戏关卡信息和模式
/// </summary>
public static class LevelReturnCode
{
    /// <summary>
    /// 冒险模式 - 存储冒险模式关卡信息
    /// </summary>
    public static Dictionary<int, AdventureInfo> AdventureModeDict = new Dictionary<int, AdventureInfo>();

    /// <summary>
    /// 环境模式 - 存储环境模式关卡信息
    /// </summary>
    public static Dictionary<int, EnvironmentInfo> EnvironmentModeDict = new Dictionary<int, EnvironmentInfo>();

    /// <summary>
    /// 小游戏模式 - 存储小游戏模式关卡信息
    /// </summary>
    public static Dictionary<int, string> MiniGameModeDict = new Dictionary<int, string>();

    /// <summary>
    /// 游戏模式 - 当前游戏模式
    /// </summary>
    public static GameMode CurrentGameMode;

    /// <summary>
    /// 图鉴模式 - 当前图鉴模式
    /// </summary>
    public static IllustratedMode CurrentIllustratedMode;

    static LevelReturnCode()
    {
        // 添加冒险模式关卡（level < 50）
        AddAdventureMode(1, "1 - 1", NormalGameType.GrasslandDay);
        AddAdventureMode(2, "1 - 2", NormalGameType.GrasslandDay);
        AddAdventureMode(3, "1 - 3", NormalGameType.GrasslandDay);
        AddAdventureMode(4, "1 - 4", NormalGameType.GrasslandDay);
        AddAdventureMode(5, "1 - 5", NormalGameType.GrasslandDay);
        AddAdventureMode(6, "1 - 6", NormalGameType.GrasslandDay);
        AddAdventureMode(7, "1 - 7", NormalGameType.GrasslandDay);
        AddAdventureMode(8, "1 - 8", NormalGameType.GrasslandDay);
        AddAdventureMode(9, "1 - 9", NormalGameType.GrasslandDay);
        AddAdventureMode(-1, "换页符", NormalGameType.GrasslandDay);
        AddAdventureMode(10, "2 - 1", NormalGameType.GrasslandNight);
        AddAdventureMode(11, "2 - 2", NormalGameType.GrasslandNight);
        AddAdventureMode(12, "2 - 3", NormalGameType.GrasslandNight);
        AddAdventureMode(13, "2 - 4", NormalGameType.GrasslandNight);
        AddAdventureMode(14, "2 - 5", NormalGameType.GrasslandNight);
        AddAdventureMode(15, "2 - 6", NormalGameType.GrasslandNight);
        AddAdventureMode(16, "2 - 7", NormalGameType.GrasslandNight);

        //AddAdventureMode(300, "测试关卡", NormalGameType.GrasslandNight);

        // 添加环境模式关卡（level >= 50），环境类型均为 Forest
        AddEnvironmentMode(51, "森林 - 1 - 1", EnvironmentType.Forest);
        AddEnvironmentMode(52, "森林 - 1 - 2", EnvironmentType.Forest);
        AddEnvironmentMode(53, "森林 - 1 - 3", EnvironmentType.Forest);
        AddEnvironmentMode(54, "森林 - 1 - 4", EnvironmentType.Forest);
        AddEnvironmentMode(55, "森林 - 1 - 5", EnvironmentType.Forest);
        AddEnvironmentMode(56, "森林 - 1 - 6", EnvironmentType.Forest);
        AddEnvironmentMode(57, "森林 - 2 - 1", EnvironmentType.Forest);
        AddEnvironmentMode(58, "森林 - 2 - 2", EnvironmentType.Forest);
        AddEnvironmentMode(59, "森林 - 3 - 1", EnvironmentType.Forest);
        //AddEnvironmentMode(-1, "换页符", EnvironmentType.Other);
        //AddEnvironmentMode(81, "钢铁 - 1 - 1", EnvironmentType.Steel);

        // 小游戏模式示例（已有代码）
        //MiniGameModeDict.Add(241, "随机礼盒");
        //MiniGameModeDict.Add(242, "冰天雪地");
        MiniGameModeDict.Add(242, "真正的排山倒海！");
        MiniGameModeDict.Add(243, "排山倒海");

        MiniGameModeDict.Add(244, "树桩的梦想·一");
        MiniGameModeDict.Add(245, "树桩的梦想·二");
        MiniGameModeDict.Add(246, "树桩的梦想·三");
        MiniGameModeDict.Add(247, "树桩的梦想·娱乐模式");

        MiniGameModeDict.Add(248, "滑步的诀窍！");

        MiniGameModeDict.Add(253, "狂轰滥炸！·一");

        MiniGameModeDict.Add(-1, "换页符");
        MiniGameModeDict.Add(201, "<color=red>嫁接联动：\n辣椒机枪射手！</color>");
        MiniGameModeDict.Add(202, "<color=green>杂交联动：\n树人僵尸！</color>");
        // MiniGameModeDict.Add(244, "   苹果迫击炮挑战   ");
    }

    /// <summary>
    /// 添加环境模式 - 添加环境模式关卡信息
    /// </summary>
    /// <param name="level">关卡等级</param>
    /// <param name="name">关卡名称</param>
    /// <param name="environmentType">环境类型</param>
    public static void AddEnvironmentMode(int level, string name, EnvironmentType environmentType)
    {
        EnvironmentInfo info = new EnvironmentInfo
        {
            Name = name,
            Type = environmentType
        };

        EnvironmentModeDict.Add(level, info);
    }

    /// <summary>
    /// 添加冒险模式 - 添加冒险模式关卡信息
    /// </summary>
    /// <param name="level">关卡等级</param>
    /// <param name="name">关卡名称</param>
    /// <param name="normalGameType">普通游戏类型</param>
    public static void AddAdventureMode(int level, string name, NormalGameType normalGameType)
    {
        AdventureInfo info = new AdventureInfo
        {
            Name = name,
            Type = normalGameType
        };

        AdventureModeDict.Add(level, info);
    }
}

/// <summary>
/// 环境信息 - 存储环境模式关卡的信息
/// </summary>
public class EnvironmentInfo
{
    public string Name { get; set; }
    public EnvironmentType Type { get; set; }
}

/// <summary>
/// 冒险信息 - 存储冒险模式关卡的信息
/// </summary>
public class AdventureInfo
{
    public string Name { get; set; }
    public NormalGameType Type { get; set; }
}

/// <summary>
/// 游戏模式 - 定义游戏的不同模式
/// </summary>
public enum GameMode
{
    /// <summary>
    /// 冒险模式
    /// </summary>
    AdventureMode,

    /// <summary>
    /// 小游戏模式
    /// </summary>
    MiniGameMode,

    /// <summary>
    /// 无尽模式
    /// </summary>
    EndlessMode,

    /// <summary>
    /// 环境模式
    /// </summary>
    EnvironmentMode
}

/// <summary>
/// 图鉴模式 - 定义图鉴的类型
/// </summary>
public enum IllustratedMode
{
    /// <summary>
    /// 植物图鉴
    /// </summary>
    PlantIllustrated,

    /// <summary>
    /// 僵尸图鉴
    /// </summary>
    ZombieIllustrated,
}

/// <summary>
/// 普通游戏类型 - 定义普通游戏的场景类型
/// </summary>
public enum NormalGameType
{
    /// <summary>
    /// 草地白天
    /// </summary>
    GrasslandDay,

    /// <summary>
    /// 草地夜晚
    /// </summary>
    GrasslandNight
}



