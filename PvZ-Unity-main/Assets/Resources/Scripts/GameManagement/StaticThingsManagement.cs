using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticThingsManagement
{
    public static int melonParasiticedIncrease = 0;//�����϶����˺��ӳ�
    public static bool forestRedZombie = false;
    public static int forestBuff;
    public static GameObject glovePlant = null;
    public static bool ������ֲ = false;
    public static List<GameObject> �����ϴ�ѡ���� = new List<GameObject>();

    public static bool �򿪶������� = false;


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
