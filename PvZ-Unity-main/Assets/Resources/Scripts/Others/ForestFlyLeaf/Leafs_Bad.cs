using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafs_Bad : MonoBehaviour
{
    public void OnEnable()//�������Ӿ綾�˺�
    {
        foreach (GameObject z in ZombieManagement.���Ͻ�ʬ)
        {

            if (z != null)
            {
                Zombie zombie = z.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.�л���״̬(true);
                }


                if (GameManagement.GameDifficult >= 3) {
                    ForestZombie forestzombie = z.GetComponent<ForestZombie>();
                    if (forestzombie != null)
                    {
                        forestzombie.Ѫ�� *= 2;
                        forestzombie.���Ѫ�� *= 2;
                    }

                }
               
            }
            else { ZombieManagement.���Ͻ�ʬ.Remove(z); }
        }

    }
}
