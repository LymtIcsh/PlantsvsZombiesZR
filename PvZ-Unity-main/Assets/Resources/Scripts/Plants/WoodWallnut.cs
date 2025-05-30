using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWallNut : WallNut
{
    public GameObject SpineBullet;
    public int ���Ͳ���;

   
    public override int beAttacked(int hurt, string form, GameObject zombieObject)
    {

        beAttackedMoment(hurt, form, zombieObject);
        TriggerHighlight();
        if (Armor > 0)
        {

            int ArmorDamage = Mathf.Min(hurt, Armor);
            Armor -= ArmorDamage;
            hurt -= ArmorDamage;
        }
        if (hurt > 0)
        {
            Ѫ�� -= hurt;
            �������();
            
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
       

        for (int i = 0; i < 8; i++) {
            GameObject spineBullet = Instantiate(SpineBullet, transform.position, Quaternion.identity);
            Vector2 Way;
            switch(i){
                case 0:Way = new Vector2(1, 0);break;
                case 1: Way = new Vector2(1, 1); break;
                case 2: Way = new Vector2(0, 1); break;
                case 3: Way = new Vector2(-1, 1); break;
                case 4: Way = new Vector2(-1, 0); break;
                case 5: Way = new Vector2(-1, -1); break;
                case 6: Way = new Vector2(0, -1); break;
                case 7: Way = new Vector2(1, -1); break;
                default: Way = new Vector2(1, 0); break;
            }
              spineBullet.GetComponent<SpecialStraightBullet>().TheWay = Way;
        }
        ����Ѫ���ı�();
        return Ѫ��;

    }
    public override void recover(int value)
    {
        base.recover(value/5);
    }
    private void �������() {
        if (GameManagement.levelData.levelEnviornment == "Forest")
        {

            Armor += ���Ͳ��� * 50;
            ����Ѫ���ı�();
            forestSlider.IncreaseSliderValueSmooth(2);
        }
        else
        {
            Armor += ���Ͳ��� * 20;
        }

    }
}
