using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestScreenDoorZombie : Zombie
{
    protected override void HandleLevel2ArmorDamage(int hurt)
    {
        base.HandleLevel2ArmorDamage(hurt);
        zombieForestSlider.DecreaseSliderValueSmooth(2);
    }



}

