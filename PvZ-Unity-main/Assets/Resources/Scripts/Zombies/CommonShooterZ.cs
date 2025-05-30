using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonShooterZ : Zombie
{
    public int BulletCount;//һ��������ٿ��ӵ�
    public GameObject Bullet;
    public GameObject ShootPoint;//�����



    protected virtual void fireEvent() {
        if(!dying)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                // �����ӵ����� i ��λ��ƫ��
                GameObject bullet = Instantiate(Bullet,
                    ShootPoint.transform.position,
                    Quaternion.Euler(0, 0, 0));

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if (!debuff.�Ȼ�)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // ˮƽ��ת�ӵ� (��ת X �������)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }

            }
        }
        
    }
}
