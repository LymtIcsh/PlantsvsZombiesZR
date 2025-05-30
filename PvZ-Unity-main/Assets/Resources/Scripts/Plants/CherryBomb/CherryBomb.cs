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
        // 1) 从池里取出（或新建）——这个对象已经是实例化好的
        GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.CherryBigBoom);

        // 2) 设置位置和朝向
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;

        // 3) 激活并拿到你的 Boom 脚本
        go.SetActive(true);
        Boom bombBoom = go.GetComponent<Boom>();
        bombBoom.row = row;
        die("",gameObject);
    }
}
