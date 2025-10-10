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
    void EnterShoot() {//每8秒射击一次
        myAnimator.SetBool("Shoot", true);
    
    }
    public void GetTarget() {
        TargetPlant = null;
        int bloodMin = 100000;//存储血量的变量
        for (int i = 0; i < PlantManagement.PlantsInFieldList.Count; i++) {
            print("寻找植物");
            if (PlantManagement.PlantsInFieldList[i] == null) { continue; }
            else {//遍历每个植物的血量
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
    public void Shoot() {//锁定血量最少的植物并进行攻击
        
        if (TargetPlant != null)
        {
            TargetPlant.beAttacked(500, null, null);//造成500点伤害，刚好秒脆皮
            GameObject blast= GameObject.Instantiate(Blast, TargetPlant.transform.position, Quaternion.identity);
           
        }
    
        myAnimator.SetBool("Shoot", false);
        Invoke("EnterShoot", 5);
    }

}
