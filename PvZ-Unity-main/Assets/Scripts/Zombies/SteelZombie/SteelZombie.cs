using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelZombie : Zombie
{
    protected override float EnvironmentSpeedZone
    {
        get => 0.75f;
        set => base.EnvironmentSpeedZone = value;
    }
}
