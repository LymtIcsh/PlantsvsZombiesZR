using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStraightBullet : StraightBullet
{

    //非行类型子弹，可以朝着任意方向直线移动，且攻击僵尸无行限制
    public Vector2 TheWay = new Vector2(1,0);

    void Start() {
        float angle = Mathf.Atan2(TheWay.y, TheWay.x) * Mathf.Rad2Deg;//物体与X轴的夹角
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    

    protected override void Update()
    {

        if (!boomState)
        {
            //因为能朝着任意方向，故不再设置阵营
            transform.Translate(speed * Time.deltaTime,0,0);
        
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Camp == 0)//植物方的子弹
        {
            if (collision.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = collision.GetComponent<Zombie>();

                if (zombieGeneric != null && zombieGeneric.buff.Stealth == false) // 如果是 Zombie
                {
                    if (boomState == false)
                    {

                        attack(zombieGeneric);  // 改为调用 attack(Zombie)
                    }
                }

            }
        }
        else
        {
            if (collision.tag == "Plant" && collision.GetComponent<Plant>()._plantType == PlantType.NormalPlants)
            {

                if (peaType == 0)
                {
                    System.Random random = new System.Random();
                    System.Random rand = new System.Random();
                    int result = rand.Next(2, 4);
                    AudioManager.Instance.PlaySoundEffect(result);
                }
                else if (peaType == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(30);
                }

                Plant plant = collision.GetComponent<Plant>();
                plant.beAttacked(hurt, "BeHit", null);
                boom();
            }

        }
        if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);
        }
    }





}
