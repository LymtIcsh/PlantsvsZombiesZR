using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunZ : Zombie
{
    //我会喷火，你会吗

    public FireGun myFireGun1;

    protected override void Start()
    {
        base.Start();
        myFireGun1.pos_row = this.pos_row;
        myFireGun1.myFire.gameObject.SetActive(false);
    }


    protected override void Update()
    {              
        if (myFireGun1.myFire.Plants.Count > 0)//火焰燎到植物就停下来
        {
           myAnimator.SetBool("Walk", false);
           myAnimator.SetBool("Attack", true);
          
        }
        else {
            myAnimator.SetBool("Attack", false);
           myAnimator.SetBool("Walk", true);
        }
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)//冲就完事了
    {
        if (collision.tag == "GameOverLine")
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
    }


    public override void 附加减速() // 自身免疫冰冻
    {
      
     
    }
}
