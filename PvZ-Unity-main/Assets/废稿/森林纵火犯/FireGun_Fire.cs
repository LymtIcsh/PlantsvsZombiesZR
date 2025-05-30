using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Audio;
using Vector3 = UnityEngine.Vector3;

public class FireGun_Fire : MonoBehaviour
{
    public GameObject FatherZombie;

    //���ƶ���
    public int pos_row;
    public int attack;
    public List<Plant> Plants = new List<Plant>();//������ֲ���


    // Update is called once per frame
    void Update()
    {
        if (Plants.Count > 0)
        {
            GetComponent<Animator>().SetBool("Attack", true);

        }
        else
        {
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {//�����յش�
            Plants.Add(collision.GetComponent<Plant>());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {//�����յش�
            Plants.Remove(collision.GetComponent<Plant>());
        }


    }

    private void Attack()
    {

        for (int i = 0; i < Plants.Count; i++)
        {

            if (Plants[i].row == this.pos_row)
            {
                Plants[i].beAttacked(attack, null, FatherZombie);
            }
        }
    }

}