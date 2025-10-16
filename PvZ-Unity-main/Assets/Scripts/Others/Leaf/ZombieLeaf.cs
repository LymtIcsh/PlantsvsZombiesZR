using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLeaf : ForestZombie
{
    public GameObject leafDrops;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ZombieDisappearLine")
        {
            Destroy(gameObject);
            ZombieManagement.instance.minusZombieNumAll(gameObject);
        }
    }

    protected override void doAfterStartSomeTimes()
    {
        die();
        zombieForestSlider.DecreaseSliderValueSmooth(20);
    }

    public override void die()
    {
        if (alive)
        {
            alive = false;
            //碰撞体失效
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (GameManagement.instance.zombieManagement != null)
            {
                //全场僵尸数减一
                GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
            }
            Destroy(gameObject);
        }
    }

    public override void beAttacked(int hurt, int BulletType, int AttackedMusicType)
    {
        base.beAttacked(hurt, BulletType, AttackedMusicType);
        startToBurn();
    }

    public void init(int pos_row)
    {
        this.pos_row = pos_row;
    }

    public void startToBurn()
    {
        if (zombieForestSlider != null)
        {
            zombieForestSlider.DecreaseSliderValueSmooth(1);
        }
        
        if (!GameManagement.isPerformance)
        {
            GameObject dropLeaf = Instantiate(leafDrops, gameObject.transform.position, Quaternion.identity);
        }
    }


    public void returnStatusToFalse()
    {
        
        

    }


    public void good()
    {

    }

    public void bad()
    {

    }
}
