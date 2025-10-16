using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShooterPaperZombie : PaperZombie
{
    //会射击的二爷僵尸
    public GameObject SuperFirePea;//超级火焰子弹
    public GameObject JalapenoBullet;//火爆辣椒子弹
    public GameObject ShootPoint;
    public int EnengyMax;
    private int energy=0;

    protected override void Anger()
    {//二爷生气了
        myAnimator.SetBool("Lost", true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands").gameObject.SetActive(false);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands2").gameObject.SetActive(true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_outerarm_hand").gameObject.SetActive(true);
    }

    protected override void HandleLevel1ArmorDamage(int hurt)//此类二爷报纸视为一类护甲
    {
       
        if (level1ArmorHealth <= level1ArmorMaxHealth * 2 / 3 && !Level1ArmorHalfDamagedSwitched)
        {
            loadPaper(2);
            Level1ArmorHalfDamagedSwitched = true;
        }
        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 && !Level1ArmorFullyDamagedSwitched)
        {
            loadPaper(3);
        }
        if (level1ArmorHealth <= 0 && LostPaper==false)
        {
            loadPaper(0);
            
        }
        // 这里可以添加二类防具受伤后的逻辑，例如播放二类防具的损坏动画、音效等
        //Debug.Log($"二类防具受到 {hurt} 点伤害");
    }
    protected override void HandleLevel2ArmorDamage(int hurt) { }


    protected virtual void fireEvent()
    {
        if (alive && Angry && !dying)
        {

            if (energy < EnengyMax)
            {
                // 生成子弹
                GameObject bullet = Instantiate(SuperFirePea,
                        ShootPoint.transform.position,
                        Quaternion.Euler(0, 0, 0));

                

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if(!debuff.Charmed)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // 水平翻转子弹 (反转 X 轴的缩放)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }
                
                energy++;
            }
            else
            {
                // 生成子弹
                GameObject bullet = Instantiate(JalapenoBullet,
                        ShootPoint.transform.position,
                        Quaternion.Euler(0, 0, 0));

                

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if (!debuff.Charmed)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // 水平翻转子弹 (反转 X 轴的缩放)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }
                    
                energy = 0;
            }

        }

    }
    public override void ApplyDeceleration() // 自身免疫冰冻
    {


    }
}
