using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
   protected float crackedPoint1, crackedPoint2;

    protected override void Start()
    {
        base.Start();

        crackedPoint1 = 血量 * 2 / 3;
        crackedPoint2 = 血量 / 3;
    }
    public override int beAttacked(int hurt, string form, GameObject zombieObject)
    {
        beAttackedMoment(hurt, form, zombieObject);
        TriggerHighlight();
        //若处于强化状态，防御力增强
        if (intensified) hurt = (int)(hurt * 0.75);
        if (Armor > 0) {
            int ArmorDamage = Mathf.Min(hurt, Armor);
            Armor -= ArmorDamage;
            hurt -= ArmorDamage;
        }
        if (hurt > 0)
        {
            血量 -= hurt;
          
            if (血量 <= 0)
            {
                die(form, gameObject);
            }
            else if (血量 <= crackedPoint2)
            {

                GetComponent<Animator>().SetBool("Cracked2", true);
            }
            else if (血量 <= crackedPoint1)
            {
                GetComponent<Animator>().SetBool("Cracked1", true);
            }
        }
        加载血量文本();
        return 血量;
    }

    public override void recover(int value)
    {
        血量 += value;
        if (血量 > 最大血量) 血量 = 最大血量;

        if (血量 >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (血量 <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (血量 <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (血量 <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }

        加载血量文本();

    }

    public override void increaseMaxHP(int value)
    {
        base.increaseMaxHP(value);

        if (血量 >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (血量 <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (血量 <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (血量 <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }
    }




}
