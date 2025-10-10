using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ForestSmallPuff : SmallPuff
{
    public GameObject Smoke;

    public override void AfterDestroy()
    {
        Instantiate(Smoke, transform.position, Quaternion.identity);
        Collider2D[] zombies = Physics2D.OverlapCircleAll(base.transform.position, 0.3f);    foreach (Collider2D thezombie in zombies) {
            if (thezombie.CompareTag("Zombie"))
            {
                // �ж��Ƿ��� Zombie ����
                Zombie zombieGeneric = thezombie.GetComponent<Zombie>();

                if (zombieGeneric != null && row == zombieGeneric.pos_row) // ����� Zombie
                {
                    zombieGeneric.ApplyPoison(10);
                }

            }

        }

    }

}
