using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPuff : Shroom
{
    public GameObject pea;  //�ӵ�Ԥ����
    public GameObject createPeaPosition;
    public void fireEvent()
    {
        //�����㶹
        Instantiate(pea,
                    createPeaPosition.transform.position,
                    Quaternion.Euler(0, 0, 0))
            .GetComponent<StraightBullet>().initialize(row);

        AudioManager.Instance.PlaySoundEffect(21);

    }

}
