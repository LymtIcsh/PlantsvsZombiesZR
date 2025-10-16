using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletAnimationSwitch : StraightBullet
{
    protected override void boom()
    {
        //�ӵ�ը��
        gameObject.GetComponent<Animator>().SetBool("Boom", true);
        
        transform.Find("Shadow").gameObject.SetActive(false);
        boomState = true;
    }
}
