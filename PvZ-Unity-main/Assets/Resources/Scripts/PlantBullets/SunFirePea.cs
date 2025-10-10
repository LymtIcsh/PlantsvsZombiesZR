using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SunFirePea : StraightBulletAnimationSwitch
{

    //���Դ�͸�Ļ��򣬼���������
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
        if (Camp == 0)//ֲ�﷽���ӵ�
        {
            if (collision.CompareTag("Zombie"))
            {
                // �ж��Ƿ��� Zombie ����
                Zombie zombieGeneric = collision.GetComponent<Zombie>();
              
                if (zombieGeneric != null && row == zombieGeneric.pos_row) // ����� Zombie
                {
                    if (boomState == false)
                    {
                        boom();
                        attack(zombieGeneric);  // ��Ϊ���� attack(Zombie)
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
                if (plant.GetComponent<DiamonWood>() == null) { //���Ա�ɭ����׮�����Ҳ����ٴ�͸
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
