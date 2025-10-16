using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomShroom : Plant
{
    protected override void Start()
    {
        base.Start();
        animator.SetBool("Idle", false);
    }

    public void Boom()
    {
        AudioManager.Instance.PlaySoundEffect(58);
        // 1) �ӳ���ȡ�������½���������������Ѿ���ʵ�����õ�
        GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.Doom);

        // 2) ����λ�úͳ���x���ȥ0.1��
        Vector3 spawnPosition = transform.position;
        spawnPosition.x -= 0.2f;
        spawnPosition.y -= 0.2f;
        go.transform.position = spawnPosition;
        go.transform.rotation = Quaternion.identity;

        // 3) ����õ���� Boom �ű�
        go.SetActive(true);
        Boom bombBoom = go.GetComponent<Boom>();
        bombBoom.row = row;
        die("", gameObject);

        myGrid.SpawnCrater();
    }
}
