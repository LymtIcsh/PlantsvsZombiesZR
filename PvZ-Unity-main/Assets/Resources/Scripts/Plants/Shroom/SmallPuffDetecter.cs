using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDetecter : DetectZombieRegion
{
    //预定好右边缘攻击的范围检测
    protected override void Start()
    {
        // 缓存 Animator 组件
        plantAnimator = myPlant.GetComponent<Animator>();

        // 判断 Animator 中 Attack 参数类型
        isTriggerAttack = false;
        foreach (var param in plantAnimator.parameters)
        {
            if (param.name == "Attack")
            {
                if (param.type == AnimatorControllerParameterType.Trigger)
                    isTriggerAttack = true;
                break;
            }
        }

        // 如果是 Trigger，每1.4秒检查并触发攻击
        if (isTriggerAttack)
        {
            StartCoroutine(RepeatedAttack());
        }

        myCollider.enabled = true;
    }
    public override void 重新计算区域()//用于如果Glove移动
    {
      
        myCollider.enabled = false;
        StopAttack();
        zombiesInRegion.Clear();
        myCollider.enabled = true;

    }
}
