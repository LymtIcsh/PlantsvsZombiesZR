using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamonWood : Plant
{
    public int BePlantAttacked = 25;
    public GameObject FlowersunPrefab;   //向日葵太阳预制体

    Transform sunManagement;   //太阳管理器对象Transform组件，为所有太阳父对象

    public GameObject Leaf;
    public GameObject diamonPea;
    public bool CanCreateDiamonPea;
    public Collider2D burnRegionCollider;


    protected override void Start()
    {
        base.Start();
        warm();
        sunManagement = GameManagement.instance.sunManagement.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirePea") && collision.GetComponent<StraightBullet>().row == row)
        {
            forestSlider.DecreaseSliderValueSmooth(2);
            if (!GameManagement.isPerformance)
            {
                // 播放破碎效果
                GameObject shatterEffect = Instantiate(Leaf, transform.position, Quaternion.identity);
            }
            // 生成钻石豌豆
            GameObject diamondpea = Instantiate(diamonPea, collision.transform.position, Quaternion.identity);
            diamondpea.GetComponent<StraightBullet>().initialize(row);

            Destroy(collision.gameObject);  // 销毁子弹

            beAttacked(BePlantAttacked, null, gameObject);

            GameObject flowersunPrefab = Instantiate(FlowersunPrefab, transform.position, Quaternion.Euler(0, 0, 0), sunManagement);
        }
        else if (collision.CompareTag("Pea") && collision.GetComponent<StraightBullet>().row == row)
        {
            forestSlider.DecreaseSliderValueSmooth(2);
            if (!GameManagement.isPerformance)
            {

                // 播放破碎效果
                GameObject shatterEffect = Instantiate(Leaf, transform.position, Quaternion.identity);
            }


            // 生成钻石豌豆
            Instantiate(diamonPea, collision.transform.position, Quaternion.identity)
                .GetComponent<StraightBullet>().initialize(row);

            Destroy(collision.gameObject);  // 销毁子弹

            beAttacked(BePlantAttacked, null, gameObject);

            // 判断植物是否死亡
            if (Health <= 0)
            {
                die(null, gameObject);
            }
        }
    }


}
