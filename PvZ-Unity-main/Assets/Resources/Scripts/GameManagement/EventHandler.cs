using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<SteelBonus> TriggerEnvironmentalBonuses;//������ͼ�ӳ��¼������ݼӳ��з�Ӧ��������ע����¼�
    public static void CallTriggerEnvironmentalBonuses(SteelBonus steelBonusType)
    {
        TriggerEnvironmentalBonuses?.Invoke(steelBonusType);
    }

    public static event Action GameStart;
    public static void CallGameStart()
    {
        GameStart?.Invoke();
    }

    //public static event Action<ZombieCount> 
}
