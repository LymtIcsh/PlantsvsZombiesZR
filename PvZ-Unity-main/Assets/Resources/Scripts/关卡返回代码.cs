using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class 关卡返回代码
{
    public static Dictionary<int, 冒险信息> 冒险模式 = new Dictionary<int, 冒险信息>();
    public static Dictionary<int, 环境信息> 环境模式 = new Dictionary<int, 环境信息>();
    public static Dictionary<int, string> 小游戏模式 = new Dictionary<int, string>();
    public static 游戏模式 游戏模式;
    public static 图鉴模式 图鉴模式;
    static 关卡返回代码()
    {
        // 添加冒险模式关卡（level < 50）
        添加冒险模式(1, "1 - 1", NormalGameType.草地白天);
        添加冒险模式(2, "1 - 2", NormalGameType.草地白天);
        添加冒险模式(3, "1 - 3", NormalGameType.草地白天);
        添加冒险模式(4, "1 - 4", NormalGameType.草地白天);
        添加冒险模式(5, "1 - 5", NormalGameType.草地白天);
        添加冒险模式(6, "1 - 6", NormalGameType.草地白天);
        添加冒险模式(7, "1 - 7", NormalGameType.草地白天);
        添加冒险模式(8, "1 - 8", NormalGameType.草地白天);
        添加冒险模式(9, "1 - 9", NormalGameType.草地白天);
        添加冒险模式(-1, "换页符", NormalGameType.草地白天);
        添加冒险模式(10, "2 - 1", NormalGameType.草地夜晚);
        添加冒险模式(11, "2 - 2", NormalGameType.草地夜晚);
        添加冒险模式(12, "2 - 3", NormalGameType.草地夜晚);
        添加冒险模式(13, "2 - 4", NormalGameType.草地夜晚);
        添加冒险模式(14, "2 - 5", NormalGameType.草地夜晚);
        添加冒险模式(15, "2 - 6", NormalGameType.草地夜晚);
        添加冒险模式(16, "2 - 7", NormalGameType.草地夜晚);

        //添加冒险模式(300, "测试关卡", NormalGameType.草地夜晚);

        // 添加环境模式关卡（level >= 50），环境类型均为 Forest
        添加环境模式(51, "森林 - 1 - 1", EnvironmentType.Forest);
        添加环境模式(52, "森林 - 1 - 2", EnvironmentType.Forest);
        添加环境模式(53, "森林 - 1 - 3", EnvironmentType.Forest);
        添加环境模式(54, "森林 - 1 - 4", EnvironmentType.Forest);
        添加环境模式(55, "森林 - 1 - 5", EnvironmentType.Forest);
        添加环境模式(56, "森林 - 1 - 6", EnvironmentType.Forest);
        添加环境模式(57, "森林 - 2 - 1", EnvironmentType.Forest);
        添加环境模式(58, "森林 - 2 - 2", EnvironmentType.Forest);
        添加环境模式(59, "森林 - 3 - 1", EnvironmentType.Forest);
        //添加环境模式(-1, "换页符", EnvironmentType.Other);
        //添加环境模式(81, "钢铁 - 1 - 1", EnvironmentType.Steel);

        // 小游戏模式示例（已有代码）
        //小游戏模式.Add(241, "随机礼盒");
        //小游戏模式.Add(242, "冰天雪地");
        小游戏模式.Add(242, "真正的排山倒海！");
        小游戏模式.Add(243, "排山倒海");
        
        小游戏模式.Add(244, "树桩的梦想·一");
        小游戏模式.Add(245, "树桩的梦想·二");
        小游戏模式.Add(246, "树桩的梦想·三");
        小游戏模式.Add(247, "树桩的梦想·娱乐模式");

        小游戏模式.Add(248, "滑步的诀窍！");

        小游戏模式.Add(253, "狂轰滥炸！·一");

        小游戏模式.Add(-1, "换页符");
        小游戏模式.Add(201, "<color=red>嫁接联动：\n辣椒机枪射手！</color>");
        小游戏模式.Add(202, "<color=green>杂交联动：\n树人僵尸！</color>");
        // 小游戏模式.Add(244, "   苹果迫击炮挑战   ");
    }

    public static void 添加环境模式(int level,string name,EnvironmentType environmentType)
    {
        环境信息 信息 = new 环境信息
        {
            Name = name,
            Type = environmentType
        };

        环境模式.Add(level, 信息);
    }
    public static void 添加冒险模式(int level, string name, NormalGameType normalGameType)
    {
         冒险信息 信息 = new 冒险信息
        {
            Name = name,
            Type = normalGameType
        };

        冒险模式.Add(level, 信息);
    }
}

public class 环境信息
{
    public string Name { get; set; }
    public EnvironmentType Type { get; set; }

    
}

public class 冒险信息
{
    public string Name { get; set; }
    public NormalGameType Type { get; set; }


}

public enum 游戏模式
{
    冒险模式,
    小游戏模式,
    无尽模式,
    环境模式
}

public enum 图鉴模式
{
    植物图鉴,
    僵尸图鉴,
}

public enum NormalGameType
{
    草地白天,
    草地夜晚
}



