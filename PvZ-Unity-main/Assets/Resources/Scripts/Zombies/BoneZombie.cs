using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneZombie : Zombie
{
    int lifeNumber = 3;   //剩余几条命

    protected override void Start()
    {
        base.Start();
    }

    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        血量 -= hurt;
        if (血量 <= 0)
        {
            split();
        }
    }

    private void split()
    {
        //碰撞体失效
        gameObject.GetComponent<Collider2D>().enabled = false;
        //命减少
        lifeNumber--;
        if (lifeNumber <= 0)
        {
            //全场僵尸数减一
            ZombieManagement.instance.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
            //僵尸消失
            Invoke("disappear", 2f);
        }
        //动画切换
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Die", true);
        //随机时间后复活
        Invoke("revive", Random.Range(20.0f, 30.0f));
    }

    private void revive()
    {
        //动画切换
        myAnimator.SetBool("Die", false);
        myAnimator.SetBool("Walk", true);
        //血量恢复
        血量 = 最大血量;
        //碰撞体生效
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
