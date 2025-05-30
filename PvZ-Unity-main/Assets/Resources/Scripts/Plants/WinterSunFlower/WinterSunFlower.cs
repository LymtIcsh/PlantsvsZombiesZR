using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterSunFlower : Plant
{
    public GameObject flowersunPrefab;   //向日葵太阳预制体
    public GameObject flowerPrefab;
    public GameObject thawGameObject;//解冻时特效

    Transform sunManagement;   //太阳管理器对象Tranform组件，为所有太阳父对象
    float createSunSpeed = 1f;
    float thawTime = 10f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sunManagement = GameManagement.instance.sunManagement.transform;

        Invoke("createSun", 1);
    }

    private void Update()
    {
        thawTime -= Time.deltaTime;
        if(thawTime <= 0)
        {
            die("",gameObject);
        }
    }

    private void createSun()
    {

        //生成太阳
        Instantiate(flowersunPrefab, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);

        Invoke("createSun", createSunSpeed);
    }


    public override void cold()
    {
        base.cold();
        createSunSpeed = 48f;
    }

    public override void warm()
    {
        base.warm();
        createSunSpeed = 24f;
    }

    public override void normal()
    {
        base.normal();
        createSunSpeed = 24f;
    }

    public override void AfterDestroy()
    {
        createNormalSunFlower();
    }

    private void createNormalSunFlower()
    {
        SetAchievement.SetAchievementCompleted("植物大战僵尸退化版");
        GetComponentInParent<PlantGrid>().plantByGod("SunFlower");
        if(!GameManagement.isPerformance)
        {
            GameObject show = Instantiate(thawGameObject, transform.position, Quaternion.identity);
        }
        
            
    }





}
