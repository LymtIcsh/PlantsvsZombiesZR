using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class arrow : StraightBullet
{
    // Start is called before the first frame update
    void Start()
    {  
        Destroy(gameObject,1f);
    }

    // Update is called once per frame
   

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Camp == 0)//植物方的子弹
        {
            if (collision.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = collision.GetComponent<Zombie>();

                if (zombieGeneric != null && row == zombieGeneric.pos_row && !zombieGeneric.debuff.Charmed) // 如果是 Zombie
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
            if (collision.tag == "Plant" && row == collision.GetComponent<Plant>().row && collision.GetComponent<Plant>()._plantType ==  PlantType.NormalPlants)
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











    protected override void attack(Zombie target)
    {     
        //僵尸被攻击
        target.beAttacked(hurt+target.debuff.Poison*20, 1, 1);
        boom();
    }

}
