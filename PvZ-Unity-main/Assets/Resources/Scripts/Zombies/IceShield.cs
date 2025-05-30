using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : Zombie
{
    private GameObject myZombie;

    protected override void Awake()
    {
        ////��ȡ���
        //audioSource = gameObject.GetComponent<AudioSource>();
    }

   
    public override void die()
    {
        AudioManager.Instance.PlaySoundEffect(35);
        if (myZombie != null)
        {
            Animator animator = myZombie.GetComponent<Animator>();
            if (animator.GetBool("Attack") == false)
            {
                animator.SetBool("Walk", true);
            }
        }
        Destroy(gameObject);
    }

    public override void beBurned(int damage)
    {
        �������״̬();
        beAttacked(damage*20, 1, 1);
    }

    public override void beSquashed()
    {
        Ѫ�� -= 1800;
        if (Ѫ�� <= 0)
        {
            //��ʬ��ʧ
            Destroy(gameObject);
        }
    }

    public void init(int pos_row, GameObject zombie)
    {
        this.pos_row = pos_row;
        GetComponent<SpriteRenderer>().sortingLayerName = "Zombie-" + pos_row;

        this.myZombie = zombie;
    }


    public override void ���Ӽ���()
    {
        
    }
}
