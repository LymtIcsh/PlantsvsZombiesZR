using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashDetectRegion : MonoBehaviour
{
    public Squash squash;   // 窝瓜

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰撞体是否是 Zombie 或 Zombie
        if (collision.CompareTag("Zombie"))
        {
            //Zombie zombie = collision.GetComponent<Zombie>();
            Zombie zombieGeneric = collision.GetComponent<Zombie>();

            if (zombieGeneric != null && squash.row == zombieGeneric.pos_row) // 处理 Zombie 类型
            {
                if (squash.collider_Idle.enabled)
                {
                    // 锁定僵尸
                    squash.lockedZombie = collision.gameObject;

                    // 判断僵尸在左边还是右边
                    if (squash.lockedZombie.transform.position.x > transform.position.x)
                        squash.animator.SetBool("LookRight", true);
                    else
                        squash.animator.SetBool("LookLeft", true);

                    squash.collider_Idle.enabled = false;   // 探测碰撞体失效
                    squash.idle = false;
                }
                else if (squash.collider_attack.enabled)
                {
                    zombieGeneric.beSquashed(); // 对 Zombie 执行攻击
                }
            }
        }
    }
}
