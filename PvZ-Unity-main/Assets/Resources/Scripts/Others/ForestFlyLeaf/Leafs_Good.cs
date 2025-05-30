using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leafs_Good : MonoBehaviour
{
    public void OnEnable()//启用增加剧毒伤害
    {
        Debug.Log("启用");
        foreach (GameObject zm in ZombieManagement.场上僵尸.ToList())
        {
            Zombie zms = zm.GetComponent<Zombie>();
            if (zms != null)
            {
                if (GameManagement.levelData.LevelType == levelType.TheDreamOfWood)
                {
                    zms.附加中毒(10);
                    zms.引爆毒伤(1);
                }
                else
                {
                    zms.附加中毒(50);
                }
            }
        }

    }

    public void OnDisable()//禁用减少剧毒伤害
    {
    }
}
