using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRepeaterZ : Zombie
{
    public int BulletCount;//һ��������ٿ��ӵ�
    public GameObject Bullet;
    public GameObject ShootPoint;//�����



    protected virtual void fireEvent() {

        for (int i = 0; i < BulletCount; i++)
        {
            // �����ӵ����� i ��λ��ƫ��
            GameObject bullet = Instantiate(Bullet,
                ShootPoint.transform.position + new Vector3(-0.2f * i, 0, 0),
                Quaternion.Euler(0, 0, 0));

            bullet.GetComponent<StraightBullet>().Camp = 1;//��ʬ�����ӵ��������֮ǰ��int����ȷ������Ӫ

            // ��ȡ�ӵ��������ʼ��
            bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

            // ˮƽ��ת�ӵ� (��ת X �������)
            bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
            bullet.GetComponent<StraightBullet>().speed *= -1;
            //������Ч
            //audioSource.Play();


            //�����ӵ�ˮƽ��ת�����������Ų�����
        }
    }
}
