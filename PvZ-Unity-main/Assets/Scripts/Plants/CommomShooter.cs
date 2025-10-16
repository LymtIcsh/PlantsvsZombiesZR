using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonShooter : Plant
{
    public GameObject pea;  //子弹预制体
    public GameObject createPeaPosition;
    public void fireEvent()
    {
        //生成豌豆
        //ObjectPoolManager.Instance.SpawnFromPool(pea.name, createPeaPosition.transform.position, Quaternion.Euler(0, 0, 0))
        //    .GetComponent<StraightBullet>().initialize(row);
        Instantiate(pea,
                    createPeaPosition.transform.position,
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<StraightBullet>().initialize(row);

        //播放音效
        AudioManager.Instance.PlaySoundEffect(21);

    }

    //protected override void Start()
    //{
    //    base.Start();
    //    float syncTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;
    //    animator.Play("SecondLayerAnimation", 1, syncTime);
    //}

}
