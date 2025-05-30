using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafs_Bad : MonoBehaviour
{
    public void OnEnable()//启用增加剧毒伤害
    {
        foreach (GameObject z in ZombieManagement.场上僵尸)
        {

            if (z != null)
            {
                Zombie zombie = z.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.切换狂暴状态(true);
                }


                if (GameManagement.GameDifficult >= 3) {
                    ForestZombie forestzombie = z.GetComponent<ForestZombie>();
                    if (forestzombie != null)
                    {
                        forestzombie.血量 *= 2;
                        forestzombie.最大血量 *= 2;
                    }

                }
               
            }
            else { ZombieManagement.场上僵尸.Remove(z); }
        }

    }
}
