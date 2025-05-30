using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Plant
{
    public ChomperDetectZombieRegion chomperDetectZombieRegion;
    public void biteZombie()//用于检查子物体的攻击僵尸是否还存在，决定动画是返回Idle还是进行咀嚼
    {
        AudioManager.Instance.PlaySoundEffect(54);
        //return为真代表无僵尸，为假代表有僵尸
        bool isReturn = chomperDetectZombieRegion.zombie == null || chomperDetectZombieRegion.zombie.dying || !chomperDetectZombieRegion.zombie.alive;
        Debug.Log(isReturn);
        animator.SetBool("Return", isReturn);
        if(!isReturn)
        {
            chomperDetectZombieRegion.zombie.beChompered();
            chomperDetectZombieRegion.afterBiteZombie(!isReturn);
        }
        else
        {
            chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = false;
            chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = true;
        }
    }

    public override void 重新计算攻击碰撞箱()
    {
        base.重新计算攻击碰撞箱();
        chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = false;
        chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = true;
    }
}
