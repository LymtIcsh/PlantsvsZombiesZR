using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWallNut : WallNut
{
    public GameObject SpineBullet;
    public int 坚韧层数;

   
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
            血量 -= hurt;
            计算坚韧();
            
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
        加载血量文本();
        return 血量;

    }
    public override void recover(int value)
    {
        base.recover(value/5);
    }
    private void 计算坚韧() {
        if (GameManagement.levelData.levelEnviornment == "Forest")
        {

            Armor += 坚韧层数 * 50;
            加载血量文本();
            forestSlider.IncreaseSliderValueSmooth(2);
        }
        else
        {
            Armor += 坚韧层数 * 20;
        }

    }
}
