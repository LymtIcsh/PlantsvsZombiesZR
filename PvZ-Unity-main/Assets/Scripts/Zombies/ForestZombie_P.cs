using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestZombie_P : ForestZombie
{
    //��Ⱦɭ�ֽ�ʬ

    protected override void Start()
    {
        base.Start();
        buff.Toughness = 4;
       
    }

    protected override void HandleBodyDamage(int hurt)
    {
        // ������Դ��������˺���߼�������۳�Ѫ��������������Ч��������
        Health -= hurt; // �����Ѷȼ���
        //Debug.Log($"�����ܵ� {hurt} ���˺���ʣ��Ѫ��: {bloodVolume}");
        // ������������ӱ������˺�������߼������粥�����˶�������Ч��
        CalculateToughness();
    }


    protected override void doAfterStartSomeTimes()//��Ȼ�����Ǳض�������Ⱦ�ݴ�
    {
        if (alive)
        {
            zombieForestSlider.DecreaseSliderValueSmooth(7);
            GameObject zombieLeaf = Instantiate(ZombieLeaf, gameObject.transform.position, Quaternion.identity);
            zombieLeaf.GetComponent<ZombieLeaf>().init(pos_row);
            die();
        }

    }

}
