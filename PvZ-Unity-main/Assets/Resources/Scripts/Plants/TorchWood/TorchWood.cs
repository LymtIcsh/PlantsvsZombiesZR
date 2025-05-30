


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchWood : Plant
{
    public GameObject firePea;
    public GameObject diamonPea;
    public bool CanCreateDiamonPea;
    public int firePeaHurt = 40;

    protected override void Start()
    {
        base.Start();

        warm();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pea")
        {
            if (collision.GetComponent<StraightBullet>().row == row)
            {
                Instantiate(firePea,
                            collision.transform.position,
                            Quaternion.Euler(0, 0, 0))
                    .GetComponent<StraightBullet>().initialize(row, firePeaHurt);
                //�����㶹
                Destroy(collision.gameObject);
            }
            
        }
        else if (collision.tag == "FirePea")
        {

            if (collision.GetComponent<StraightBullet>().row == row)
            {
                if (CanCreateDiamonPea)
                {
                    Instantiate(diamonPea,
                        collision.transform.position,
                        Quaternion.Euler(0, 0, 0))
                        .GetComponent<StraightBullet>().initialize(row);
                    Destroy(collision.gameObject);
                }
            }

            

        }
    }


    //protected override void beforeDie()
    //{
    //    transform.Find("WarmPlantRegion").GetComponent<WarmPlantRegion>().stopWarm();
    //}

    protected override void intensify_specific()
    {
        GetComponent<Animator>().speed = 1.5f;
        firePeaHurt = (int)(firePeaHurt * 1.5);
    }

    protected override void cancelIntensify_specific()
    {
        GetComponent<Animator>().speed = 1f;
        firePeaHurt = (int)(firePeaHurt / 1.5);
    }
}

