using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePea : StraightBulletAnimationSwitch
{
    // 攻击方法，传入的目标是 Zombie 类型
    //protected override void attack(Zombie target)
    //{
    //    if (target != null)
    //    {
    //        // 给目标僵尸造成伤害
    //        target.beAttacked(hurt, true);
    //        target.beBurned(); // 触发烧伤效果
    //    }
    //}

    // 攻击方法，传入的目标是 Zombie 类型
    protected override void attack(Zombie target)
    {

        if (target != null)
        {
            // 给目标僵尸造成伤害           
            boom();
            target.beBurned(hurt); // 触发烧伤效果
        }
    }
}
