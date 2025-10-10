using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HunterZombie : Zombie
{
    public GameObject ShootImage;
    public GameObject shootPoint;
    public GameObject Blast;

    public Plant TargetPlant;

    GameObject shootImage;
    protected override void Start()
    {
        base.Start();
     
       Invoke("EnterShoot",8);
    }
    void EnterShoot() {//ÿ8�����һ��
        myAnimator.SetBool("Shoot", true);
    
    }
    public void GetTarget() {
        TargetPlant = null;
        int bloodMin = 100000;//�洢Ѫ���ı���
        for (int i = 0; i < PlantManagement.PlantsInFieldList.Count; i++) {
            print("Ѱ��ֲ��");
            if (PlantManagement.PlantsInFieldList[i] == null) { continue; }
            else {//����ÿ��ֲ���Ѫ��
                if (PlantManagement.PlantsInFieldList[i].GetComponent<Plant>().Health <= bloodMin) {
                    TargetPlant = PlantManagement.PlantsInFieldList[i].GetComponent<Plant>();
                    bloodMin = PlantManagement.PlantsInFieldList[i].GetComponent<Plant>().Health;
                }

            }
        }

       
            shootImage = Instantiate(ShootImage, shootPoint.transform.position, Quaternion.Euler(0, 0, 0));
            shootImage.transform.DOMove(TargetPlant.transform.position, 1f);
        Destroy(shootImage.gameObject,1.2f);
    }
    public void Shoot() {//����Ѫ�����ٵ�ֲ�ﲢ���й���
        
        if (TargetPlant != null)
        {
            TargetPlant.beAttacked(500, null, null);//���500���˺����պ����Ƥ
            GameObject blast= GameObject.Instantiate(Blast, TargetPlant.transform.position, Quaternion.identity);
           
        }
    
        myAnimator.SetBool("Shoot", false);
        Invoke("EnterShoot", 5);
    }

}
