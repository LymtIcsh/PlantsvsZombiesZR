using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafs_Bad : MonoBehaviour
{
    public void OnEnable()//∆Ù”√‘ˆº”æÁ∂æ…À∫¶
    {
        foreach (GameObject z in ZombieManagement.zombiesOnField)
        {

            if (z != null)
            {
                Zombie zombie = z.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.SwitchFuriousState(true);
                }


                if (GameManagement.GameDifficult >= 3) {
                    ForestZombie forestzombie = z.GetComponent<ForestZombie>();
                    if (forestzombie != null)
                    {
                        forestzombie.Health *= 2;
                        forestzombie.MaxHealth *= 2;
                    }

                }
               
            }
            else { ZombieManagement.zombiesOnField.Remove(z); }
        }

    }
}
