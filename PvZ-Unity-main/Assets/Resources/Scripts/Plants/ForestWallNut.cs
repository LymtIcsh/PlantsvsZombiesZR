using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWallNut :  WallNut
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        LoadHealthText();
        InvokeRepeating("RecoverItself",0f,10f);
    }



    public override void recover(int value)
    {
        base.recover(2 * value);
    }

    public override void beAttackedMoment(int hurt, string form, GameObject zombieObject)
    {
        if (zombieObject != null)
        {
            zombieObject.GetComponent<Zombie>().ApplyPoison(1);
        }
    }

    private void RecoverItself() {//×Ô»ØÑª

        recover(5);
    }

}
