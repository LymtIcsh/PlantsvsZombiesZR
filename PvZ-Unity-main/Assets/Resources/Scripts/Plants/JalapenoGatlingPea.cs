using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalapenoGatlingPea : CommonShooter
{
    public override void AfterDestroy()
    {
        base.AfterDestroy();
        GameObject fire = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.Fire);
        fire.GetComponent<Boom>().row = row;
        Vector3 spawnPosition = transform.position;
        fire.transform.position = spawnPosition;
        fire.transform.rotation = Quaternion.identity;
        AudioManager.Instance.PlaySoundEffect(63);
    }
}
