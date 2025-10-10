using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leafs_Good : MonoBehaviour
{
    public void OnEnable()//∆Ù”√‘ˆº”æÁ∂æ…À∫¶
    {
        Debug.Log("∆Ù”√");
        foreach (GameObject zm in ZombieManagement.zombiesOnField.ToList())
        {
            Zombie zms = zm.GetComponent<Zombie>();
            if (zms != null)
            {
                if (GameManagement.levelData.LevelType == levelType.TheDreamOfWood)
                {
                    zms.ApplyPoison(10);
                    zms.DetonatePoisonDamage(1);
                }
                else
                {
                    zms.ApplyPoison(50);
                }
            }
        }

    }

    public void OnDisable()//Ω˚”√ºı…ŸæÁ∂æ…À∫¶
    {
    }
}
