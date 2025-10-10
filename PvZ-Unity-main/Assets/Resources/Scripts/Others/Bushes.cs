using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bushes : Zombie
{
    public GameObject Sun;

    protected override void Start()
    {
        base.Start();
        int StartTime = Random.Range(100, 150);
        Invoke("CreateZombie", StartTime);
        ZombieManagement.zombiesOnField.Remove(this.gameObject);

        GetComponent<SortingGroup>().sortingLayerName = "EnvironmentThings-" + pos_row;
        
        ChangeHealthDisplay(false);
    }
    #region 碰撞
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            Zombie z = collision.GetComponent<Zombie>();
            if (z != null && z.alive)
            {
                if (z.pos_row == pos_row)
                {
                    z.buff.Stealth = true;
                    myAnimator.SetBool("Shake", true);
                }
            }
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<DiamonPea>() != null)
        {
            print("被打");
            if (collision.GetComponent<DiamonPea>().row == this.pos_row)
            {
                beAttacked(collision.GetComponent<DiamonPea>().hurt, 1, 1);
                Destroy(collision.gameObject);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            Zombie z = collision.GetComponent<Zombie>();
            if (z != null && z.alive)
            {
                if (collision.tag == "Zombie" && z.pos_row == pos_row)
                {
                    myAnimator.SetBool("Shake", false);
                    z.buff.Stealth = false;
                }
            }
        }
    }
    #endregion 

    private void CreateZombie()
    {
        if (alive)
        {
            GameObject randomZombie = ZombieManagement.instance.GenerateZombieInlevel();


            //plantGrid = GetComponentInParent<PlantGrid>();
            //在当前格子位置生成一个随机僵尸
            Vector3 vector3 = gameObject.transform.position;
            vector3.y = GameManagement.levelData.zombieInitPosY[pos_row];
            vector3.z = 0;
            GameObject spawnedZombie = Instantiate(randomZombie, vector3, Quaternion.identity, GameManagement.instance.zombieManagement.transform);
            spawnedZombie.GetComponent<Zombie>().pos_row = this.pos_row;
            spawnedZombie.GetComponent<Zombie>().setPosRow(this.pos_row);//设置图层
            int randTime = Random.Range(30, 50);
            Invoke("CreateZombie", randTime);
        }

    }


    public override void die()
    {

        if (alive)
        {
            zombieForestSlider.DecreaseSliderValueSmooth(-20);
            
            alive = false;
            myAnimator.SetBool("Die", true);



            for (int i = 0; i < 4; i++)
            {
                Instantiate(Sun, transform.position, Quaternion.identity);
            }
        }
    }
}
