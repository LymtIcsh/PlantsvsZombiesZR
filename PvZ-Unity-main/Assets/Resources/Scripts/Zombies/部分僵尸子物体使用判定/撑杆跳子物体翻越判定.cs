using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 撑杆跳子物体翻越判定 : MonoBehaviour
{
    public bool 已经翻越 = false;
    public int row;
    public Plant plant;
    public PlantGrid plantGrid;//防止植物被移走，在撑杆跳僵尸处更新最新植物

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!已经翻越 && collision.tag == "Plant")
        {
            plant = collision.GetComponent<Plant>();
            if(plant != null && plant.row == row && plant.植物类型 != PlantType.地刺类植物)
            {
                plantGrid = plant.myGrid;
                GetComponentInParent<Zombie>().可以啃咬 = false;
                已经翻越 = true;
                GetComponentInParent<Animator>().SetBool("Fly", true);
                GetComponentInParent<Animator>().SetBool("Walk", true);
            }
            
        }
    }

}
