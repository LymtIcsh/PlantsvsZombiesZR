using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MultiImagePlant
{
    //苹果迫击炮  
    public List<GameObject> bullets;

    private Vector3 bulletOffset = new Vector3(0.656f, 0.392f, 0);   //子弹初始位置偏移量
    private Vector3 castEndPoint;   //射线投射时终点

    public GameObject ShootPoint;//炮弹生成位置

    protected override void Start()
    {
        base.Start();
        castEndPoint = new Vector3(5.3f, transform.position.y, transform.position.z);
    }

    public void fireEvent()
    {
        
        for (int i = 0; i < 3; i++)
        {
            // 使用射线检测三条线上的僵尸
            RaycastHit2D[] hitResults =
                Physics2D.LinecastAll(transform.position + new Vector3(0, i * 0.8f - 0.8f, 0),
                castEndPoint + new Vector3(0, i * 0.8f - 0.8f, 0), LayerMask.GetMask("Zombie"));


            foreach (RaycastHit2D hitResult in hitResults)
            {

                // 获取僵尸组件
                Zombie zombieGeneric = hitResult.transform.GetComponent<Zombie>();
                //Zombie zombie = hitResult.transform.GetComponent<Zombie>(); // 获取 Zombie



                if (zombieGeneric != null)
                {
                    if (zombieGeneric.pos_row >= row - 1 && zombieGeneric.pos_row <= row + 1)
                    {
                        // 选择一个随机子弹类型
                        int bulletIndex = Random.Range(0, bullets.Count);
                        GameObject bullet = Instantiate
                            (
                            bullets[bulletIndex],
                            ShootPoint.transform.position,
                            Quaternion.identity
                        );


                        bullet.GetComponent<ThrowBullet>().initialize(zombieGeneric, this, zombieGeneric.pos_row); // 传递 Zombie
                        break;
                    }
                }
                //else if (zombie != null)
                //{
                //    if (zombie.pos_row >= row - 1 && zombie.pos_row <= row + 1)
                //    {
                //        {
                //            // 如果碰到的是 Zombie 类型的僵尸
                //            int bulletIndex = Random.Range(0, bullets.Count);
                //            GameObject bullet = Instantiate(
                //                bullets[bulletIndex],
                //                ShootPoint.transform.position,
                //                Quaternion.identity
                //            );


                //            bullet.GetComponent<ThrowBullet>().initialize(zombie, this, zombie.pos_row); // 传递 Zombie

                //            audioSource.Play();
                //            break;
                //        }
                //    }
                //}
            }
        }
    }
}
