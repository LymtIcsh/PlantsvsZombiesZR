using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneZombie : Zombie
{
    int lifeNumber = 3;   //ʣ�༸����

    protected override void Start()
    {
        base.Start();
    }

    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        Ѫ�� -= hurt;
        if (Ѫ�� <= 0)
        {
            split();
        }
    }

    private void split()
    {
        //��ײ��ʧЧ
        gameObject.GetComponent<Collider2D>().enabled = false;
        //������
        lifeNumber--;
        if (lifeNumber <= 0)
        {
            //ȫ����ʬ����һ
            ZombieManagement.instance.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
            //��ʬ��ʧ
            Invoke("disappear", 2f);
        }
        //�����л�
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Die", true);
        //���ʱ��󸴻�
        Invoke("revive", Random.Range(20.0f, 30.0f));
    }

    private void revive()
    {
        //�����л�
        myAnimator.SetBool("Die", false);
        myAnimator.SetBool("Walk", true);
        //Ѫ���ָ�
        Ѫ�� = ���Ѫ��;
        //��ײ����Ч
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
