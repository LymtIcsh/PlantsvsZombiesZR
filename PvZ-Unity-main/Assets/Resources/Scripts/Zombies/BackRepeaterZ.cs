using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRepeaterZ : Zombie
{
    public int BulletCount;//一次射击多少颗子弹
    public GameObject Bullet;
    public GameObject ShootPoint;//射击点



    protected virtual void fireEvent() {

        for (int i = 0; i < BulletCount; i++)
        {
            // 生成子弹，按 i 有位置偏移
            GameObject bullet = Instantiate(Bullet,
                ShootPoint.transform.position + new Vector3(-0.2f * i, 0, 0),
                Quaternion.Euler(0, 0, 0));

            bullet.GetComponent<StraightBullet>().Camp = 1;//僵尸方的子弹，这个是之前以int类型确定的阵营

            // 获取子弹组件并初始化
            bullet.GetComponent<StraightBullet>().initialize(this.pos_row);

            // 水平翻转子弹 (反转 X 轴的缩放)
            bullet.transform.localScale = new Vector3(-Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
            bullet.GetComponent<StraightBullet>().speed *= -1;
            //播放音效
            //audioSource.Play();


            //建议子弹水平翻转，这样火球看着才正常
        }
    }
}
