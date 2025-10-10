using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Zombie
{

    protected override void Start()
    {
        base.Start();
        AudioManager.Instance.PlaySoundEffect(19);
        //�����ں���ƺ�����λ��
        transform.localPosition = new Vector3( Random.Range(3.0f,5.0f), transform.localPosition.y, 0);
        InvokeRepeating("Hidding",0f,1f);
    }

    protected override void Update()
    {
        base.Update();
        buff.Stealth = true;
    }

    //��дΪ��
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!debuff.Charmed && collision.tag == "GameOverLine")
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
    }


   
}
