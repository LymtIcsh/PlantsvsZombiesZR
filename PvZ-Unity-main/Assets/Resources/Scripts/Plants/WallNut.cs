using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
   protected float crackedPoint1, crackedPoint2;

    protected override void Start()
    {
        base.Start();

        crackedPoint1 = Health * 2 / 3;
        crackedPoint2 = Health / 3;
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
            Health -= hurt;
          
            if (Health <= 0)
            {
                die(form, gameObject);
            }
            else if (Health <= crackedPoint2)
            {

                GetComponent<Animator>().SetBool("Cracked2", true);
            }
            else if (Health <= crackedPoint1)
            {
                GetComponent<Animator>().SetBool("Cracked1", true);
            }
        }
        LoadHealthText();
        return Health;
    }

    public override void recover(int value)
    {
        Health += value;
        if (Health > MaxHealth) Health = MaxHealth;

        if (Health >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (Health <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (Health <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (Health <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }

        LoadHealthText();

    }

    public override void increaseMaxHP(int value)
    {
        base.increaseMaxHP(value);

        if (Health >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (Health <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (Health <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (Health <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }
    }




}
