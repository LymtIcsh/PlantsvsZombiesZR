using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class JalapenoBullet : StraightBullet
{
    public GameObject JalanopeFireLine;


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
                        Explosion(0, this.hurt,row);
                    }
                }
              
            }
        }
        else
        {
            if (collision.tag == "Plant" && row == collision.GetComponent<Plant>().row && collision.GetComponent<Plant>()._plantType == PlantType.NormalPlants)
            {

                if (peaType == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(30);
                }

                Plant plant = collision.GetComponent<Plant>();
                if (plant.GetComponent<DiamonWood>() != null)
                {
                    Explosion(0, this.hurt, row);
                }
                else {
                    Explosion(1, this.hurt, row);
                }

                boom();

            }

        }
        if (collision.CompareTag("BulletDisappearLine"))
        {
            Destroy(gameObject);
        }
    }

    private void Explosion(int camp,int attack,int row) {

        GameObject jalanopefireline = Instantiate(JalanopeFireLine, transform.position, Quaternion.identity);
        jalanopefireline.GetComponent<Jalapeno_FireLine>().Camp = camp;
        jalanopefireline.GetComponent<Jalapeno_FireLine>().Attack = attack;
        jalanopefireline.GetComponent<Jalapeno_FireLine>().Row = row;
    }

}
