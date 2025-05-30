using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingZombie : Zombie
{
    public BowlingBall_Zombie BowlingBall;
    public GameObject ShootPoint;
    protected virtual void fireEvent()
    {
        if(debuff.÷È»ó)
            return;
        BowlingBall_Zombie ball = Instantiate(BowlingBall,
                ShootPoint.transform.position,
                Quaternion.identity);
    }
}

