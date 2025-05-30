using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FumeShroom : Shroom
{
    public GameObject Shoot;
    public GameObject Fume;
    public GameObject EndPoint;

    public virtual void fireEvent()
    {
        AudioManager.Instance.PlaySoundEffect(60);

        GameObject fume =Instantiate(Fume
                   );
        Vector2 vector2 = Shoot.transform.position;
        //vector2.y -= 0.04f;
        fume.transform.position = vector2;

        RaycastHit2D[] hitResults =
              Physics2D.LinecastAll(transform.position,
               EndPoint.transform.position, LayerMask.GetMask("Zombie"));

        for (int i = 0; i < hitResults.Length; i++) {
            // 获取僵尸组件
            Zombie zombieGeneric = hitResults[i].transform.GetComponent<Zombie>();
           
            if (zombieGeneric != null && !zombieGeneric.debuff.魅惑)
            {
                if (zombieGeneric.pos_row == row )
                {
                  

                    zombieGeneric.beAttacked(20, 2, 0);
                }
            }
        }
    }
}
