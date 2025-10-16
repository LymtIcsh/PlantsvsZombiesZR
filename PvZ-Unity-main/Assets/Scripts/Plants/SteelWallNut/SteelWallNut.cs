using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SteelWallNut : WallNut
{
    public GameObject wallNutPrefab;

    public GameObject showGameObject;//����ʱ��Ч



    public override void beAttackedMoment(int hurt, string form, GameObject zombieObject)
    {
        if (zombieObject != null)
        {
            if (zombieObject.GetComponent<Zombie>() != null)
            {
                // ��ȡ��ʬ��ǰλ��
                Vector3 zombiePosition = zombieObject.transform.position;
                // �޸� x ����
                zombiePosition.x += 0.3f;
                // ���¸�ֵ�� transform.position
                zombieObject.transform.position = zombiePosition;

                int adjustedHurt = Mathf.Min(hurt / 2, 100);//���һ�ٵ��˺�
                                                            // ���� Zombie �� beAttacked ����
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
