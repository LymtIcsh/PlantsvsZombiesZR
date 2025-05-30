using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombie3Row : MonoBehaviour
{
    public GameObject myPlant;
    public BoxCollider2D myCollider;
    private HashSet<GameObject> zombiesInRange = new HashSet<GameObject>();  // 储存当前范围内的僵尸

    private void Start()
    {
        // 设置碰撞体的大小和偏移量
        float rightEdge = 5.3f;
        float leftEdge = myPlant.transform.position.x;
        myCollider.size = new Vector2(rightEdge - leftEdge, myCollider.size.y);
        myCollider.offset = new Vector2((rightEdge - leftEdge) / 2, 0);
        myCollider.enabled = true;
    }

    // 碰撞进入事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是僵尸，并且其行数与植物的行数匹配
        if (collision.CompareTag("Zombie"))
        {
            Zombie zombieGeneric = collision.GetComponent<Zombie>();  // 获取Zombie组件
            //Zombie zombie = collision.GetComponent<Zombie>();  // 获取Zombie组件（如果有的话）

            bool isInAttackRange = false;  // 标记僵尸是否在攻击范围内

            // 判断僵尸是否在攻击范围内（行数匹配）
            if (zombieGeneric != null && zombieGeneric.pos_row >= myPlant.GetComponent<Plant>().row - 1
                && zombieGeneric.pos_row <= myPlant.GetComponent<Plant>().row + 1)
            {
                isInAttackRange = true;
            }
            //else if (zombie != null && zombie.pos_row >= myPlant.GetComponent<Plant>().row - 1
            //         && zombie.pos_row <= myPlant.GetComponent<Plant>().row + 1)
            //{
            //    isInAttackRange = true;
            //}

            // 如果僵尸在攻击范围内并且是第一次进入，开始攻击
            if (isInAttackRange)
            {
                zombiesInRange.Add(collision.gameObject);  // 将僵尸添加到范围内的集合中
                if (zombiesInRange.Count == 1)  // 如果是第一个进入的僵尸，开始攻击
                {
                    myPlant.GetComponent<Animator>().SetBool("Attack", true);
                }
            }
        }
    }

    // 碰撞退出事件
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 检查是否是僵尸
        if (collision.CompareTag("Zombie"))
        {
            // 只有在 zombiesInRange 中存在该物体时，才从集合中移除
            if (zombiesInRange.Contains(collision.gameObject))
            {
                zombiesInRange.Remove(collision.gameObject);  // 从集合中移除该僵尸

                // 如果没有僵尸在范围内，则停止攻击
                if (zombiesInRange.Count == 0)
                {
                    myPlant.GetComponent<Animator>().SetBool("Attack", false);  // 停止攻击动画
                }
            }
        }
    }
}
