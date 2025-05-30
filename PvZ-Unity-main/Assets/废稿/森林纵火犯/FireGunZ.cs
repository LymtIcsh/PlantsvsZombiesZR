using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunZ : Zombie
{
    //�һ���������

    public FireGun myFireGun1;

    protected override void Start()
    {
        base.Start();
        myFireGun1.pos_row = this.pos_row;
        myFireGun1.myFire.gameObject.SetActive(false);
    }


    protected override void Update()
    {              
        if (myFireGun1.myFire.Plants.Count > 0)//�����ǵ�ֲ���ͣ����
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

    protected override void OnTriggerEnter2D(Collider2D collision)//���������
    {
        if (collision.tag == "GameOverLine")
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
    }


    public override void ���Ӽ���() // �������߱���
    {
      
     
    }
}
