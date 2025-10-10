using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ForestSunFlower : SunFlower
{
    public int AttackPower;
    public GameObject forestflowersunPrefab;
    public GameObject redflowersunPrefab;


    protected override void Start()
    {
        base.Start();

    }

    public override void createSun()
    {
        base.createSun();
        Medic();
    }

    private void Medic()
    {//产生阳光,治疗周围植物并解除他们的异常状态 
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);//半径为1的圈
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.tag == "Plant")
            {

                Plant plant = collider2D.GetComponent<Plant>();
                if (plant == null) return;
                else
                {
                    if (GameManagement.instance.SunText.GetSunNum() <= 1000)
                    {
                        if (plant.GetComponent<ForestSunFlower>() != null)
                        {                         
                            GameManagement.instance.forestSlider.DecreaseSliderValueSmooth(2);

                            Instantiate(forestflowersunPrefab, plant.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
                        }
                        plant.normal();
                        plant.recover(AttackPower);
                      //  plant.beAttacked(0, null, null);//只是方便显示血量，之后请在recover加上文本更新
                    }
                    else {
                        if (plant.GetComponent<ForestSunFlower>() != null)
                        {
                           GameManagement.instance.zombieForestSlider.DecreaseSliderValueSmooth(-3); 
                           GameManagement.instance  .forestSlider.DecreaseSliderValueSmooth(2);

                           
                        }
                        Instantiate(redflowersunPrefab, plant.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
                        plant.normal();
                        plant.recover(AttackPower*2+plant.Health/20);//阳光高时强化治疗能力，但是需要消耗阳光
                    //    plant.beAttacked(0, null, null);//只是方便显示血量，之后请在recover加上文本更新

                    }
                }

            }
        }
    }

}
/*要进加SunText的函数
public int GetSunNum()
{//获取现在阳光值

    return nowSun;
}
*/
