using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leafs_Good : MonoBehaviour
{
    public void OnEnable()//�������Ӿ綾�˺�
    {
        Debug.Log("����");
        foreach (GameObject zm in ZombieManagement.���Ͻ�ʬ.ToList())
        {
            Zombie zms = zm.GetComponent<Zombie>();
            if (zms != null)
            {
                if (GameManagement.levelData.LevelType == levelType.TheDreamOfWood)
                {
                    zms.�����ж�(10);
                    zms.��������(1);
                }
                else
                {
                    zms.�����ж�(50);
                }
            }
        }

    }

    public void OnDisable()//���ü��پ綾�˺�
    {
    }
}
