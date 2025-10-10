using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spikeweed : Plant
{
    [FormerlySerializedAs("��ɭ�ֵش�")] [Header("��ɭ�ֵش�")]
    public bool isForestThorn = false;
    public int hurt;
    private HashSet<GameObject> zombiesInRange = new HashSet<GameObject>(); // ׷�ٷ�Χ�ڵĽ�ʬ


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            Zombie zombieGeneric = collision.transform.GetComponent<Zombie>();
            //Zombie zombie = collision.transform.GetComponent<Zombie>(); // ��ȡ Zombie
            if (zombieGeneric != null && zombieGeneric.pos_row == row && !zombieGeneric.debuff.Charmed)
            {
                // ��齩ʬ�Ƿ���빥����Χ
                zombiesInRange.Add(collision.gameObject); // ��ӵ���Χ�ڵĽ�ʬ����
                animator.SetBool("Attack",true);
            }
            //if (zombie != null && zombie.pos_row == row)
            //{
            //    // ��齩ʬ�Ƿ���빥����Χ
            //    zombiesInRange.Add(collision.gameObject); // ��ӵ���Χ�ڵĽ�ʬ����
            //    animator.SetBool("Attack", true);
            //}
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (zombiesInRange.Contains(collision.gameObject))
        {
            zombiesInRange.Remove(collision.gameObject);

            // ���������û�н�ʬ���Ž�����״̬��Ϊfalse
            if (zombiesInRange.Count == 0)
            {
                animator.SetBool("Attack", false); // û�н�ʬ�ڷ�Χ��ʱ��ֹͣ����
            }
        }
    }

    public void DamageZombies()
    {
        List<GameObject> zombiesCopy = new List<GameObject>(zombiesInRange);//��ֹ�ڱ���ʱ�޸ļ��ϲ��������գ���������
        foreach (GameObject zombie in zombiesCopy)
        {
            if(zombie!=null)
            {
                
                Zombie zombieGeneric = zombie.GetComponent<Zombie>();
                //if (zombie.GetComponent<Zombie>()!=null)
                //{
                //    zombie.GetComponent<Zombie>().beAttacked(hurt, true);
                //    if (��ɭ�ֵش�)
                //    {
                //        forestSlider.DecreaseSliderValueSmooth(1);
                //        zombie.GetComponent<Zombie>().beParasiticed();
                //    }
                //}
                if (zombieGeneric != null)
                {
                    zombieGeneric.beAttacked(hurt,2,2);
                    if(isForestThorn)
                    {
                        forestSlider.DecreaseSliderValueSmooth(1);
                        zombieGeneric.DetonatePoisonDamage(1);
                    }
                    
                }
            }
        }
        if (zombiesInRange.Count == 0)
        {
            animator.SetBool("Attack", false); // û�н�ʬ�ڷ�Χ��ʱ��ֹͣ����
        }
    }

    public override void Re_CalculateAttackCollisionBox()
    {
        base.Re_CalculateAttackCollisionBox();
        List<GameObject> zombiesCopy = new List<GameObject>(zombiesInRange);//��ֹ�ڱ���ʱ�޸ļ��ϲ��������գ���������
        foreach (GameObject zombie in zombiesCopy)
        {
            if (zombie != null)
            {

                Zombie zombieGeneric = zombie.GetComponent<Zombie>();
                //if (zombie.GetComponent<Zombie>() != null)
                //{
                //    if(zombie.GetComponent<Zombie>().pos_row != row)
                //    {
                //        zombiesInRange.Remove(zombie);
                //    }
                //}
                if (zombieGeneric != null)
                {
                    if (zombieGeneric.pos_row != row)
                    {
                        zombiesInRange.Remove(zombie);
                    }
                }
            }
        }
    }
}
