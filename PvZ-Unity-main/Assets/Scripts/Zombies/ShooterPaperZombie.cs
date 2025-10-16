using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShooterPaperZombie : PaperZombie
{
    //������Ķ�ү��ʬ
    public GameObject SuperFirePea;//���������ӵ�
    public GameObject JalapenoBullet;//�������ӵ�
    public GameObject ShootPoint;
    public int EnengyMax;
    private int energy=0;

    protected override void Anger()
    {//��ү������
        myAnimator.SetBool("Lost", true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands").gameObject.SetActive(false);
        FindChildByNameRecursive(gameObject.transform, "Zombie_paper_hands2").gameObject.SetActive(true);
        FindChildByNameRecursive(gameObject.transform, "Zombie_outerarm_hand").gameObject.SetActive(true);
    }

    protected override void HandleLevel1ArmorDamage(int hurt)//�����ү��ֽ��Ϊһ�໤��
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
        // ���������Ӷ���������˺���߼������粥�Ŷ�����ߵ��𻵶�������Ч��
        //Debug.Log($"��������ܵ� {hurt} ���˺�");
    }
    protected override void HandleLevel2ArmorDamage(int hurt) { }


    protected virtual void fireEvent()
    {
        if (alive && Angry && !dying)
        {

            if (energy < EnengyMax)
            {
                // �����ӵ�
                GameObject bullet = Instantiate(SuperFirePea,
                        ShootPoint.transform.position,
                        Quaternion.Euler(0, 0, 0));

                

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if(!debuff.Charmed)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // ˮƽ��ת�ӵ� (��ת X �������)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }
                
                energy++;
            }
            else
            {
                // �����ӵ�
                GameObject bullet = Instantiate(JalapenoBullet,
                        ShootPoint.transform.position,
                        Quaternion.Euler(0, 0, 0));

                

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if (!debuff.Charmed)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // ˮƽ��ת�ӵ� (��ת X �������)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }
                    
                energy = 0;
            }

        }

    }
    public override void ApplyDeceleration() // �������߱���
    {


    }
}
