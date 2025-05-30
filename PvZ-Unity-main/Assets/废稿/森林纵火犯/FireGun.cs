using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    

    public FireGun_Fire myFire;//����
    public int pos_row;
    public int Plant_Count;//��ײ��������ֲ������
    public bool Attacking;
    private void Start()
    {
        Plant_Count = 0;
        myFire.pos_row = this.pos_row;
        Attacking = false;
    }

    private void Update()
    {
        if (Plant_Count > 0 && Attacking==false )
        {
            myFire.gameObject.SetActive(true);
            Attacking = true;
           
        }
        if (Plant_Count <= 0 && Attacking == true) {
         
            myFire.gameObject.SetActive(false);
            Attacking = false;
           
        }

    }

  

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant"&& collision.GetComponent<Plant>().row == pos_row) {//�����յش�
          
            Plant_Count++;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row)
        {//�����յش�
                 
            Plant_Count--;
        }


    }
}
