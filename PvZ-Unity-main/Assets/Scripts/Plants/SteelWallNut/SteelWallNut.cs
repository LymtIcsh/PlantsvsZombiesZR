using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SteelWallNut : WallNut
{
    public GameObject wallNutPrefab;

    public GameObject showGameObject;//死亡时特效



    public override void beAttackedMoment(int hurt, string form, GameObject zombieObject)
    {
        if (zombieObject != null)
        {
            if (zombieObject.GetComponent<Zombie>() != null)
            {
                // 获取僵尸当前位置
                Vector3 zombiePosition = zombieObject.transform.position;
                // 修改 x 坐标
                zombiePosition.x += 0.3f;
                // 重新赋值给 transform.position
                zombieObject.transform.position = zombiePosition;

                int adjustedHurt = Mathf.Min(hurt / 2, 100);//最大一百点伤害
                                                            // 调用 Zombie 的 beAttacked 方法
                zombieObject.GetComponent<Zombie>().beAttacked(adjustedHurt, 1, 1);
            }
          
        }
    }

  
    public override void AfterDestroy()
    {
        createNormalWallNut();
    }

    private void createNormalWallNut()
    {
        GetComponentInParent<PlantGrid>().plantByGod("WallNut");
        if (!GameManagement.isPerformance)
        {
            GameObject show = Instantiate(showGameObject, transform.position, Quaternion.identity);
        }
    }
}
