using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestFumeShroom : FumeShroom
{
    public GameObject Smoke;
    public override void fireEvent()
    {
        AudioManager.Instance.PlaySoundEffect(60);

        GameObject fume = Instantiate(Fume
                   );
        Vector2 vector2 = Shoot.transform.position;
        //vector2.y -= 0.04f;
        fume.transform.position = vector2;


        RaycastHit2D[] hitResults =
              Physics2D.LinecastAll(transform.position,
               EndPoint.transform.position, LayerMask.GetMask("Zombie"));

        for (int i = 0; i < hitResults.Length; i++)
        {
            // 获取僵尸组件
            Zombie zombieGeneric = hitResults[i].transform.GetComponent<Zombie>();

            if (zombieGeneric != null && !zombieGeneric.debuff.Charmed)
            {
                if (zombieGeneric.pos_row == row)
                {

                    zombieGeneric.ApplyPoison(3);
                    
                    zombieGeneric.beAttacked(20, 2, 0);
                }
            }
        }
    }
    public override void AfterDestroy()
    {
        Instantiate(Smoke, transform.position, Quaternion.identity);
        Collider2D[] zombies = Physics2D.OverlapCircleAll(base.transform.position, 0.3f); foreach (Collider2D thezombie in zombies)
        {
            if (thezombie.CompareTag("Zombie"))
            {
                // 判断是否是 Zombie 类型
                Zombie zombieGeneric = thezombie.GetComponent<Zombie>();

                if (zombieGeneric != null && row == zombieGeneric.pos_row) // 如果是 Zombie
                {
                    zombieGeneric.ApplyPoison(10);
                    zombieGeneric.DetonatePoisonDamage(1);
                }

            }

        }

    }


}
