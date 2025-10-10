using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePeopleZombie : Zombie
{
    protected override void hideHead()
    {
        if(!dying)
        {
            dying = true;
        }
    }

    protected override void dropArm()
    {
        if (!armIsDrop)
        {
            armIsDrop = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isEating && buff.Stealth)
        {
            buff.Stealth = false;
        }
        else if(!isEating && !buff.Stealth)
        {
            buff.Stealth = true;
        }
    }
}
