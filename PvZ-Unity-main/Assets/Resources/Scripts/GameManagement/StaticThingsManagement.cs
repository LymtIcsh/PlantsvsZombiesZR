using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticThingsManagement
{
    public static int melonParasiticedIncrease = 0;//毒西瓜毒性伤害加成
    public static bool forestRedZombie = false;
    public static int forestBuff;
    public static GameObject glovePlant = null;
    public static bool 正在种植 = false;
    public static List<GameObject> 保存上次选择卡牌 = new List<GameObject>();

    public static bool 打开二级界面 = false;


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
