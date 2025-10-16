using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<SteelBonus> TriggerEnvironmentalBonuses;//钢铁地图加成事件，根据加成有反应的物体需注册此事件
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
