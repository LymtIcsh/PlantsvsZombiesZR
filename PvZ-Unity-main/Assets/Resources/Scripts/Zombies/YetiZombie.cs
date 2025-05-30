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
        解除减速状态();
        beAttacked(damage*2,1,1);
    }

   
    //播放僵尸倒下的音效
    //public override void fallDown()
    //{
    //    //audioSource.clip = Resources.Load<AudioClip>("Sounds/Zombies/yetifall");
    //    //audioSource.Play();
    //}

  
}
