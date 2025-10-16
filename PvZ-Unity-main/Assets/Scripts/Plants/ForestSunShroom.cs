using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSunShroom : SunShroom
{
    public GameObject Smoke;
    public GameObject ForestSun;
    public override void AfterDestroy()
    {
        Instantiate(Smoke, transform.position, Quaternion.identity);
        Collider2D[] zombies = Physics2D.OverlapCircleAll(base.transform.position, 0.1f);//半径为1的圈
        foreach (Collider2D thezombie in zombies)
        {
            if (thezombie.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = thezombie.GetComponent<Zombie>();

                if (zombieGeneric != null && row == zombieGeneric.pos_row) // 如果是 Zombie
                {
                    zombieGeneric.ApplyPoison(10);
                }

            }

        }

    }


    public override void createSun()
    {
        base.createSun();
        CreateEntraSun();
    }
      private void CreateEntraSun()
    {
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);//半径为1的圈
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.tag == "Plant")
            {

                Plant plant = collider2D.GetComponent<Plant>();
                if (plant == null) return;
                else
                {                  
                        if (plant.GetComponent<ForestSunShroom>() != null)
                        {                         
                            GameManagement.instance.forestSlider.DecreaseSliderValueSmooth(2);

                            Instantiate(ForestSun, plant.transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
                        if (!grew) {
                            ForestSun.GetComponent<FlowerSun>().sunNumber = 2;
                        }
                        }
                        plant.normal();
                        
                  
                                      
                }

            }
        }
    }



}
