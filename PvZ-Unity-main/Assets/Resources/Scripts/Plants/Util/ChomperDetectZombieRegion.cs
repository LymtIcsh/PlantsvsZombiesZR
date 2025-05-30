using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperDetectZombieRegion : MonoBehaviour
{
    public bool isChewing = false;
    public Chomper myPlant;
    public Zombie zombie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isChewing && collision.CompareTag("Zombie"))
        {
            Zombie zombieGeneric = collision.GetComponent<Zombie>();
            if (IsZombieInRow(zombieGeneric))
            {
                zombie = zombieGeneric;
                myPlant.animator.SetTrigger("Bite");
            }
        }
    }

    public void afterBiteZombie(bool haveZombie)
    {
        if (haveZombie)
        {
            isChewing = true;
            Invoke("returnToIdle", 42f);//´æÔÚ½©Ê¬£¬½øÈë¾×½À×´Ì¬£¬42Ãëºó»Ö¸´Õý³£
        }
        else
        {
            isChewing = false;
        }
    }

    private void returnToIdle()
    {
        myPlant.animator.SetTrigger("Swallow");
        isChewing = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }



    private bool IsZombieInRow(Zombie zombieGeneric)
    {
        return zombieGeneric != null && zombieGeneric.pos_row == myPlant.GetComponent<Plant>().row;
    }
}
