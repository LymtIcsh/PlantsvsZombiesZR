using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
   protected float crackedPoint1, crackedPoint2;

    protected override void Start()
    {
        base.Start();

        crackedPoint1 = Ѫ�� * 2 / 3;
        crackedPoint2 = Ѫ�� / 3;
    }
    public override int beAttacked(int hurt, string form, GameObject zombieObject)
    {
        beAttackedMoment(hurt, form, zombieObject);
        TriggerHighlight();
        //������ǿ��״̬����������ǿ
        if (intensified) hurt = (int)(hurt * 0.75);
        if (Armor > 0) {
            int ArmorDamage = Mathf.Min(hurt, Armor);
            Armor -= ArmorDamage;
            hurt -= ArmorDamage;
        }
        if (hurt > 0)
        {
            Ѫ�� -= hurt;
          
            if (Ѫ�� <= 0)
            {
                die(form, gameObject);
            }
            else if (Ѫ�� <= crackedPoint2)
            {

                GetComponent<Animator>().SetBool("Cracked2", true);
            }
            else if (Ѫ�� <= crackedPoint1)
            {
                GetComponent<Animator>().SetBool("Cracked1", true);
            }
        }
        ����Ѫ���ı�();
        return Ѫ��;
    }

    public override void recover(int value)
    {
        Ѫ�� += value;
        if (Ѫ�� > ���Ѫ��) Ѫ�� = ���Ѫ��;

        if (Ѫ�� >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (Ѫ�� <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (Ѫ�� <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (Ѫ�� <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }

        ����Ѫ���ı�();

    }

    public override void increaseMaxHP(int value)
    {
        base.increaseMaxHP(value);

        if (Ѫ�� >= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", false);
        }
        if (Ѫ�� <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", false);
        }
        if (Ѫ�� <= crackedPoint2)
        {
            GetComponent<Animator>().SetBool("Cracked2", true);
        }
        if (Ѫ�� <= crackedPoint1)
        {
            GetComponent<Animator>().SetBool("Cracked1", true);
        }
    }




}
