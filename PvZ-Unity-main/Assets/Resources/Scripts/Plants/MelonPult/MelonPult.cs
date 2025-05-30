using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MelonPult : MultiImagePlant
{
    public GameObject rolling;
    public List<GameObject> bullets;

    private Vector2 castEndPoint;   //射线投射时终点

    public GameObject Melon;//西瓜生成位置

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector2(6f, transform.position.y);
    }

    public override void initialize(PlantGrid grid, string sortingLayer, int sortingOrder)
    {
        base.initialize(grid, sortingLayer, sortingOrder);
        castEndPoint = new Vector2(6f, transform.position.y);
    }

    public void fireEvent()
    {
        //// 使用射线检测僵尸
        //RaycastHit2D[] hitResults =
        //    Physics2D.LinecastAll(transform.position, castEndPoint, LayerMask.GetMask("Zombie"));





        //if (audioSource == null)
        //{
        //    Debug.Log("AudioSource is not assigned.");
        //}

        //foreach (RaycastHit2D hitResult in hitResults)
        //{

        //    // 获取僵尸组件
        //    Zombie zombieGeneric = hitResult.transform.GetComponent<Zombie>();
        //    Zombie zombie = hitResult.transform.GetComponent<Zombie>(); // 获取 Zombie

        GameObject z = GetZombieWithMinX();
        if (z == null)
        {
            return;
        }


       
        Zombie zombieGeneric = z.transform.GetComponent<Zombie>();
        //Zombie zombie = z.transform.GetComponent<Zombie>(); // 获取 Zombie
        if (zombieGeneric != null && zombieGeneric.pos_row == row)
            {
                // 选择一个随机子弹类型
                int bulletIndex = Random.Range(0, bullets.Count);
                GameObject bullet = Instantiate
                    (
                    bullets[bulletIndex],
                    Melon.transform.position,
                    Quaternion.identity
                );

                ThrowBullet bulletScript = bullet.GetComponent<ThrowBullet>();
                bulletScript.initialize(zombieGeneric, this, row); // 传递 Zombie

            }
        //else if (zombie != null && zombie.pos_row == row)
        //    {
        //        // 如果碰到的是 Zombie 类型的僵尸
        //        int bulletIndex = Random.Range(0, bullets.Count);
        //        GameObject bullet = Instantiate(
        //            bullets[bulletIndex],
        //            Melon.transform.position,
        //            Quaternion.identity
        //        );

        //        ThrowBullet bulletScript = bullet.GetComponent<ThrowBullet>();
        //        bulletScript.initialize(zombie, this, row); // 传递 Zombie

        //        audioSource.Play();
        //    }
        ////}


    }

    public GameObject GetZombieWithMinX()
    {
        GameObject minXZombie = null;
        float minX = float.MaxValue;

        foreach (GameObject zombie in detectZombieRegion.zombiesInRegion.ToList())
        {
            if (zombie != null)
            {
                if (zombie.transform.position.x < minX)
                {
                    minX = zombie.transform.position.x;
                    minXZombie = zombie;
                }
            }
            else
            {
                detectZombieRegion.zombiesInRegion.Remove(zombie);
            }
        }

        return minXZombie;
    }

    public override void cold()
    {
        // 冷冻逻辑（如果有）
    }

    protected override void beforeDie()
    {
        //// 检查是否通过特定关卡来激活西瓜保龄球功能
        //if (LevelManagerStatic.IsLevelCompleted(61))  // 必须通过西瓜保龄球关卡才能召唤西瓜
        //{
        //    Instantiate(rolling, transform.position, Quaternion.identity);
        //}
    }
}
