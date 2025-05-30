using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonSplashing : MonoBehaviour
{
    public int splashingHurt;  // 水果溅射伤害
    public int bulletType;     // 子弹类型：1 为中毒，2 为减速
    public int 附加毒伤层数;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            // 首先检查是否是 Zombie 类型
            Zombie zombie = collision.GetComponent<Zombie>();

            if (zombie != null)  // 确保是 Zombie 类型
            {
                if (zombie.buff.隐匿 == false && !zombie.debuff.魅惑)//不能打到隐匿的僵尸
                {
                    HandleAttack(zombie);
                    zombie.beAttacked(splashingHurt, 2, 1);  // 第二个参数为攻击模式
                }
            }
        }
        
    }

    private void HandleAttack(Zombie zombie)
    {
        
        switch (bulletType)
        {
            case 0:
                break;  // 无特殊效果
            case 1:
                zombie.附加中毒(附加毒伤层数);  // 中毒
                break;
            case 2:
                zombie.附加减速();  // 减速
                break;
            default:
                break;
        }
    }

    
}
