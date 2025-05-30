using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestZombie : Zombie
{
    public GameObject ZombieLeaf;

    protected override void Start()
    {
        base.Start();
        zombieForestSlider.DecreaseSliderValueSmooth(2);
    }
    protected override void doAfterStartSomeTimes()
    {
        if(alive && GameManagement.GameDifficult >= 4)
        {
            zombieForestSlider.DecreaseSliderValueSmooth(7);
            GameObject zombieLeaf = Instantiate(ZombieLeaf, gameObject.transform.position, Quaternion.identity);
            zombieLeaf.GetComponent<ZombieLeaf>().init(pos_row);
            die();
        }

    }

    public override void attack()
    {
        //Ö²Îï±»¹¥»÷
        if (attackPlant != null && !dying)
        {
            if (debuff.¿ñ±©) { attackPlant.beAttacked(¹¥»÷Á¦ * 2, "beEated", gameObject); }
            else
            {
                attackPlant.beAttacked(¹¥»÷Á¦, "beEated", gameObject);
            }
        }
    }
}
