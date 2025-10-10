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
    {//��������,������Χֲ�ﲢ������ǵ��쳣״̬ 
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);//�뾶Ϊ1��Ȧ
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
                      //  plant.beAttacked(0, null, null);//ֻ�Ƿ�����ʾѪ����֮������recover�����ı�����
                    }
                    else {
                        if (plant.GetComponent<ForestSunFlower>() != null)
                        {
                           GameManagement.instance.zombieForestSlider.DecreaseSliderValueSmooth(-3); 
                           GameManagement.instance  .forestSlider.DecreaseSliderValueSmooth(2);

                           
                        }
                        Instantiate(redflowersunPrefab, plant.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
                        plant.normal();
                        plant.recover(AttackPower*2+plant.Health/20);//�����ʱǿ������������������Ҫ��������
                    //    plant.beAttacked(0, null, null);//ֻ�Ƿ�����ʾѪ����֮������recover�����ı�����

                    }
                }

            }
        }
    }

}
/*Ҫ����SunText�ĺ���
public int GetSunNum()
{//��ȡ��������ֵ

    return nowSun;
}
*/
