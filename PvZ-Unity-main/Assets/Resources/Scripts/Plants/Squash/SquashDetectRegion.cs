using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashDetectRegion : MonoBehaviour
{
    public Squash squash;   // �ѹ�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ж���ײ���Ƿ��� Zombie �� Zombie
        if (collision.CompareTag("Zombie"))
        {
            //Zombie zombie = collision.GetComponent<Zombie>();
            Zombie zombieGeneric = collision.GetComponent<Zombie>();

            if (zombieGeneric != null && squash.row == zombieGeneric.pos_row) // ���� Zombie ����
            {
                if (squash.collider_Idle.enabled)
                {
                    // ������ʬ
                    squash.lockedZombie = collision.gameObject;

                    // �жϽ�ʬ����߻����ұ�
                    if (squash.lockedZombie.transform.position.x > transform.position.x)
                        squash.animator.SetBool("LookRight", true);
                    else
                        squash.animator.SetBool("LookLeft", true);

                    squash.collider_Idle.enabled = false;   // ̽����ײ��ʧЧ
                    squash.idle = false;
                }
                else if (squash.collider_attack.enabled)
                {
                    zombieGeneric.beSquashed(); // �� Zombie ִ�й���
                }
            }
        }
    }
}
