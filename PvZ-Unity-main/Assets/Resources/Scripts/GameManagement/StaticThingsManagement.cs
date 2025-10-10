using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticThingsManagement
{
    public static int melonParasiticedIncrease = 0;//毒西瓜毒性伤害加成
    public static bool forestRedZombie = false;
    public static int forestBuff;
    public static GameObject glovePlant = null;

    /// <summary>
    /// 正在种植 - 标识是否正在种植植物
    /// </summary>
    public static bool IsPlanting = false;

    /// <summary>
    /// 保存上次选择卡牌 - 存储上次选择的卡牌列表
    /// </summary>
    public static List<GameObject> SavedLastSelectedCards = new List<GameObject>();

    /// <summary>
    /// 打开二级界面 - 标识是否打开了二级界面
    /// </summary>
    public static bool IsSecondaryPanelOpen = false;


    [System.Serializable]
    public class ListWrapper
    {
        public List<Data> items;
    }

    [System.Serializable]
    public class Data
    {
        public string Name;
        public string Raw;
    }
}
