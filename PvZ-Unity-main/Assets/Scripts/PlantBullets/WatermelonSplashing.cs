using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WatermelonSplashing : MonoBehaviour
{
    public int splashingHurt;  // ˮ�������˺�
    public int bulletType;     // �ӵ����ͣ�1 Ϊ�ж���2 Ϊ����
    [FormerlySerializedAs("�����ж�����")] [Header("�����ж�����")]
    public int _addPoisoningLevels;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            // ���ȼ���Ƿ��� Zombie ����
            Zombie zombie = collision.GetComponent<Zombie>();

            if (zombie != null)  // ȷ���� Zombie ����
            {
                if (zombie.buff.Stealth == false && !zombie.debuff.Charmed)//���ܴ�����Ľ�ʬ
                {
                    HandleAttack(zombie);
                    zombie.beAttacked(splashingHurt, 2, 1);  // �ڶ�������Ϊ����ģʽ
                }
            }
        }
        
    }

    private void HandleAttack(Zombie zombie)
    {
        
        switch (bulletType)
        {
            case 0:
                break;  // ������Ч��
            case 1:
                zombie.ApplyPoison(_addPoisoningLevels);  // �ж�
                break;
            case 2:
                zombie.ApplyDeceleration();  // ����
                break;
            default:
                break;
        }
    }

    
}
