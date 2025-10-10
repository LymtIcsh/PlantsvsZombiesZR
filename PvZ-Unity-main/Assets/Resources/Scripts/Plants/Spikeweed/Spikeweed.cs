using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spikeweed : Plant
{
    [FormerlySerializedAs("是森林地刺")] [Header("是森林地刺")]
    public bool isForestThorn = false;
    public int hurt;
    private HashSet<GameObject> zombiesInRange = new HashSet<GameObject>(); // 追踪范围内的僵尸


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            Zombie zombieGeneric = collision.transform.GetComponent<Zombie>();
            //Zombie zombie = collision.transform.GetComponent<Zombie>(); // 获取 Zombie
            if (zombieGeneric != null && zombieGeneric.pos_row == row && !zombieGeneric.debuff.Charmed)
            {
                // 检查僵尸是否进入攻击范围
                zombiesInRange.Add(collision.gameObject); // 添加到范围内的僵尸集合
                animator.SetBool("Attack",true);
            }
            //if (zombie != null && zombie.pos_row == row)
            //{
            //    // 检查僵尸是否进入攻击范围
            //    zombiesInRange.Add(collision.gameObject); // 添加到范围内的僵尸集合
            //    animator.SetBool("Attack", true);
            //}
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (zombiesInRange.Contains(collision.gameObject))
        {
            zombiesInRange.Remove(collision.gameObject);

            // 如果集合中没有僵尸，才将攻击状态设为false
            if (zombiesInRange.Count == 0)
            {
                animator.SetBool("Attack", false); // 没有僵尸在范围内时，停止攻击
            }
        }
    }

    public void DamageZombies()
    {
        List<GameObject> zombiesCopy = new List<GameObject>(zombiesInRange);//防止在遍历时修改集合产生报错（恼！！！！）
        foreach (GameObject zombie in zombiesCopy)
        {
            if(zombie!=null)
            {
                
                Zombie zombieGeneric = zombie.GetComponent<Zombie>();
                //if (zombie.GetComponent<Zombie>()!=null)
                //{
                //    zombie.GetComponent<Zombie>().beAttacked(hurt, true);
                //    if (是森林地刺)
                //    {
                //        forestSlider.DecreaseSliderValueSmooth(1);
                //        zombie.GetComponent<Zombie>().beParasiticed();
                //    }
                //}
                if (zombieGeneric != null)
                {
                    zombieGeneric.beAttacked(hurt,2,2);
                    if(isForestThorn)
                    {
                        forestSlider.DecreaseSliderValueSmooth(1);
                        zombieGeneric.DetonatePoisonDamage(1);
                    }
                    
                }
            }
        }
        if (zombiesInRange.Count == 0)
        {
            animator.SetBool("Attack", false); // 没有僵尸在范围内时，停止攻击
        }
    }

    public override void Re_CalculateAttackCollisionBox()
    {
        base.Re_CalculateAttackCollisionBox();
        List<GameObject> zombiesCopy = new List<GameObject>(zombiesInRange);//防止在遍历时修改集合产生报错（恼！！！！）
        foreach (GameObject zombie in zombiesCopy)
        {
            if (zombie != null)
            {

                Zombie zombieGeneric = zombie.GetComponent<Zombie>();
                //if (zombie.GetComponent<Zombie>() != null)
                //{
                //    if(zombie.GetComponent<Zombie>().pos_row != row)
                //    {
                //        zombiesInRange.Remove(zombie);
                //    }
                //}
                if (zombieGeneric != null)
                {
                    if (zombieGeneric.pos_row != row)
                    {
                        zombiesInRange.Remove(zombie);
                    }
                }
            }
        }
    }
}
