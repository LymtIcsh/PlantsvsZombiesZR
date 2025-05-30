//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FireDoorZ : ScreenDoorZombie
//{
//    //钻门僵尸

  


//    // Update is called once per frame
//    protected override void Update()
//    {
//        base.Update();

//        loadDoorStatus();
//    }




//    public override void beFrozeAttacked()
//    {
        
//    }

//    protected override void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.tag == "Plant"
//            && collision.GetComponent<Plant>().row == pos_row
//            && myAnimator.GetBool("Attack") == false
//            && collision.GetComponent<Plant>().plantType == 0
//            )
//        {
//            collision.GetComponent<Plant>().beAttacked(3000, null, this.gameObject);//第一口啃3000血，之后正常
//            base.OnTriggerEnter2D(collision);
//        }
//    }
//}