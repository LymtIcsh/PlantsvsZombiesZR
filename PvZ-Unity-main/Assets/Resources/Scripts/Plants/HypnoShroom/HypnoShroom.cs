using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoShroom : Plant
{
    public override void beAttackedMoment(int hurt, string form, GameObject zombieObject)
    {
        if (zombieObject != null && zombieObject.GetComponent<Zombie>() != null)
        {
            AudioManager.Instance.PlaySoundEffect(57);
            Instantiate(Resources.Load<GameObject>("Prefabs/Effects/MindControl/MindControl"),zombieObject.transform.position,Quaternion.identity);
            zombieObject.GetComponent<Zombie>().ÇÐ»»÷È»ó×´Ì¬();
            die("", zombieObject);
            return;
        }
        base.beAttackedMoment(hurt,form,zombieObject);
        
    }
}
