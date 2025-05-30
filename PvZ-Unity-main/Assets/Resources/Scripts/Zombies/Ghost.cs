using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Zombie
{

    protected override void Start()
    {
        base.Start();
        AudioManager.Instance.PlaySoundEffect(19);
        //出现在后半草坪中随机位置
        transform.localPosition = new Vector3( Random.Range(3.0f,5.0f), transform.localPosition.y, 0);
        InvokeRepeating("Hidding",0f,1f);
    }

    protected override void Update()
    {
        base.Update();
        buff.隐匿 = true;
    }

    //重写为空
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!debuff.魅惑 && collision.tag == "GameOverLine")
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
    }


   
}
