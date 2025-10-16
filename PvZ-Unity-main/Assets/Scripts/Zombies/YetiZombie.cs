using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiZombie : Zombie
{
    protected override void activate()
    {
        base.activate();
        //audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/yetiroar");
        //audioSource.Play();
    }

    public override void beBurned(int damage)
    {
        RemoveDecelerationState();
        beAttacked(damage*2,1,1);
    }

   
    //���Ž�ʬ���µ���Ч
    //public override void fallDown()
    //{
    //    //audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/yetifall");
    //    //audioSource.Play();
    //}

  
}
