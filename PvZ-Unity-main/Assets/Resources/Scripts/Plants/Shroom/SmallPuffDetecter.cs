using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDetecter : DetectZombieRegion
{
    //Ԥ�����ұ�Ե�����ķ�Χ���
    protected override void Start()
    {
        // ���� Animator ���
        plantAnimator = myPlant.GetComponent<Animator>();

        // �ж� Animator �� Attack ��������
        isTriggerAttack = false;
        foreach (var param in plantAnimator.parameters)
        {
            if (param.name == "Attack")
            {
                if (param.type == AnimatorControllerParameterType.Trigger)
                    isTriggerAttack = true;
                break;
            }
        }

        // ����� Trigger��ÿ1.4���鲢��������
        if (isTriggerAttack)
        {
            StartCoroutine(RepeatedAttack());
        }

        myCollider.enabled = true;
    }
    public override void ���¼�������()//�������Glove�ƶ�
    {
      
        myCollider.enabled = false;
        StopAttack();
        zombiesInRegion.Clear();
        myCollider.enabled = true;

    }
}
