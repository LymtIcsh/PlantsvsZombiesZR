using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelZombie : Zombie
{
    protected override float 环境速度乘区
    {
        get => 0.75f;
        set => base.环境速度乘区 = value;
    }
}
