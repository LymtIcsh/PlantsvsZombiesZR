using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SunFirePea : StraightBulletAnimationSwitch
{

    //可以穿透的火球，即骄阳火球
    protected override void boom()
    {
        HasShatterEffectPrefab = true;

        if (HasShatterEffectPrefab && !GameManagement.isPerformance)
        {
            if (!GameManagement.isPerformance)
            {
                GameObject shatterEffect = Instantiate(shatterEffectPrefab, transform.position, Quaternion.identity);
                Destroy(shatterEffect,0.25f);
            }

        }


        //disappear();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Camp == 0)//植物方的子弹
        {
            if (collision.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = collision.GetComponent<Zombie>();
              
                if (zombieGeneric != null && row == zombieGeneric.pos_row) // 如果是 Zombie
                {
                    if (boomState == false)
                    {
                        boom();
                        attack(zombieGeneric);  // 改为调用 attack(Zombie)
                    }
                }
               
            }
        }
        else
        {
            if (collision.tag == "Plant" && collision.GetComponent<Plant>() != null && row == collision.GetComponent<Plant>().row && collision.GetComponent<Plant>()._plantType == PlantType.NormalPlants)
            {
if (peaType == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(30);
                }

                Plant plant = collision.GetComponent<Plant>();
                if (plant.GetComponent<DiamonWood>() == null) { //可以被森林树桩反弹且不会再穿透
                    plant.beAttacked(hurt, "BeHit", null); }
                
                boom();

            }

        }
        if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);
        }
    }


}
