using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePea : StraightBulletAnimationSwitch
{
    // ���������������Ŀ���� Zombie ����
    //protected override void attack(Zombie target)
    //{
    //    if (target != null)
    //    {
    //        // ��Ŀ�꽩ʬ����˺�
    //        target.beAttacked(hurt, true);
    //        target.beBurned(); // ��������Ч��
    //    }
    //}

    // ���������������Ŀ���� Zombie ����
    protected override void attack(Zombie target)
    {

        if (target != null)
        {
            // ��Ŀ�꽩ʬ����˺�           
            boom();
            target.beBurned(hurt); // ��������Ч��
        }
    }
}
