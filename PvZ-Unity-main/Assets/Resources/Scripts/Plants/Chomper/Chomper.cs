using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Plant
{
    public ChomperDetectZombieRegion chomperDetectZombieRegion;
    public void biteZombie()//���ڼ��������Ĺ�����ʬ�Ƿ񻹴��ڣ����������Ƿ���Idle���ǽ��о׽�
    {
        AudioManager.Instance.PlaySoundEffect(54);
        //returnΪ������޽�ʬ��Ϊ�ٴ����н�ʬ
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

    public override void ���¼��㹥����ײ��()
    {
        base.���¼��㹥����ײ��();
        chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = false;
        chomperDetectZombieRegion.GetComponent<Collider2D>().enabled = true;
    }
}
