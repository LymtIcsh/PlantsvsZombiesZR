using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : Plant
{
    protected override void Start()
    {
        base.Start();
        animator.SetBool("Idle", false);
    }

    public void Boom()
    {
        AudioManager.Instance.PlaySoundEffect(55);
        // 1) �ӳ���ȡ�������½���������������Ѿ���ʵ�����õ�
        GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.CherryBigBoom);

        // 2) ����λ�úͳ���
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;

        // 3) ����õ���� Boom �ű�
        go.SetActive(true);
        Boom bombBoom = go.GetComponent<Boom>();
        bombBoom.row = row;
        die("",gameObject);
    }
}
