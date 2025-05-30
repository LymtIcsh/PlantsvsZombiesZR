using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShield : Zombie
{
    private GameObject myZombie;

    protected override void Awake()
    {
        ////获取组件
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
        解除减速状态();
        beAttacked(damage*20, 1, 1);
    }

    public override void beSquashed()
    {
        血量 -= 1800;
        if (血量 <= 0)
        {
            //僵尸消失
            Destroy(gameObject);
        }
    }

    public void init(int pos_row, GameObject zombie)
    {
        this.pos_row = pos_row;
        GetComponent<SpriteRenderer>().sortingLayerName = "Zombie-" + pos_row;

        this.myZombie = zombie;
    }


    public override void 附加减速()
    {
        
    }
}
