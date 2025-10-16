using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPuff : Shroom
{
    public GameObject pea;  //子弹预制体
    public GameObject createPeaPosition;
    public void fireEvent()
    {
        //生成豌豆
        Instantiate(pea,
                    createPeaPosition.transform.position,
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<StraightBullet>().initialize(row);

        AudioManager.Instance.PlaySoundEffect(21);

    }

}
