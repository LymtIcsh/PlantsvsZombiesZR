using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levelType { CommonPlanting,TheDreamOfWood,TheDreamOfPotatoMine, FaithHill,None }
public class LevelData
{
    public int level;  //LevelNumber
    public string levelName;   //�ؿ���

    public string levelEnviornment;
    public string mapSuffix;  //��ͼͼƬ��׺
    public int rowCount;   //�ܹ�����
    public int landRowCount;   //����½��
    public bool isDay;   //�Ƿ����
    public string plantingManagementSuffix;   //��Ӧ����ֲ���������׺
    public string backgroundSuffix;   //��Ӧ�������ֺ�׺

    /// <summary>
    /// һ��Ŀ��ѡ��
    /// </summary>
    public bool canSelectCardsInFirstPlaythrough = true;
    /// <summary>
    /// ��ֹ�κ���Ŀѡ��
    /// </summary>
    public bool disableCardSelection = false;

    public bool GloveHaveNoCD = false;
    public float TheSizeofNeck = 0.9f;
    public bool MustLost;
    public List<float> zombieInitPosY;   //���н�ʬ��ʼY��λ��
    public levelType LevelType = levelType.CommonPlanting;
    public bool ConveyorGame = false;
    public bool EnablesForestBushGeneration = true;
    public List<string> plantCards;   //����ֲ�￨������
    public bool DontFallSun = false;
    public int StartSunNumber = -1;//��ʼ����
}