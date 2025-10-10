using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticThingsManagement
{
    public static int melonParasiticedIncrease = 0;//�����϶����˺��ӳ�
    public static bool forestRedZombie = false;
    public static int forestBuff;
    public static GameObject glovePlant = null;

    /// <summary>
    /// ������ֲ - ��ʶ�Ƿ�������ֲֲ��
    /// </summary>
    public static bool IsPlanting = false;

    /// <summary>
    /// �����ϴ�ѡ���� - �洢�ϴ�ѡ��Ŀ����б�
    /// </summary>
    public static List<GameObject> SavedLastSelectedCards = new List<GameObject>();

    /// <summary>
    /// �򿪶������� - ��ʶ�Ƿ���˶�������
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
