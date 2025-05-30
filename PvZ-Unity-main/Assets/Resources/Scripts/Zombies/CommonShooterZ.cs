using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonShooterZ : Zombie
{
    public int BulletCount;//一次射击多少颗子弹
    public GameObject Bullet;
    public GameObject ShootPoint;//射击点



    protected virtual void fireEvent() {
        if(!dying)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                // 生成子弹，按 i 有位置偏移
                GameObject bullet = Instantiate(Bullet,
                    ShootPoint.transform.position,
                    Quaternion.Euler(0, 0, 0));

                bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

                if (!debuff.魅惑)
                {
                    bullet.GetComponent<StraightBullet>().Camp = 1;
                    // 水平翻转子弹 (反转 X 轴的缩放)
                    bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
                }

            }
        }
        
    }
}
