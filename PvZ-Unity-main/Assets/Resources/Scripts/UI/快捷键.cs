using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 快捷键 : MonoBehaviour
{
    public GameObject Shovel;
    public GameObject Glove;
    public GameObject UI控制器;
    private void Update()
    {
        // 检测按下键1或键2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!Glove.activeSelf)
            {
                Shovel.GetComponent<Shovel>().clickShovel();
            }
            else
            {
                Glove.GetComponent<Glove>().取消();
                Shovel.GetComponent<Shovel>().clickShovel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!Shovel.activeSelf)
            {
                Glove.GetComponent<Glove>().clickGlove();
            }
            else
            {
                Shovel.GetComponent<Shovel>().取消();
                Glove.GetComponent<Glove>().clickGlove();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            切换血量显示();
            
        }
        // 检测是否按下Esc键
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI控制器.GetComponent<LoginUIManagement>().ToggleGameSettings();
        }
    }

    public void 切换血量显示()
    {
        if (ZombieManagement.场上僵尸.Count > 0 || PlantManagement.场上植物.Count > 0)
        {
            GameManagement.是否显示血量 = !GameManagement.是否显示血量;
            foreach (Zombie z in ZombieManagement.allZombies.ToArray())
            {
                if (z != null)
                {
                    z.变更血量显示(GameManagement.是否显示血量);
                }

            }
            for (int i = 0; i < PlantManagement.场上植物.Count; i++)
            {
                GameObject p = PlantManagement.场上植物[i];

                if (p != null)
                {
                    Plant ps = p.GetComponent<Plant>();
                    if (ps != null)
                    {
                        ps.变更血量显示(GameManagement.是否显示血量);
                    }
                }
                else
                {
                    PlantManagement.RemovePlant(p);
                    i--;
                }
            }
        }
        else
        {
            return;
        }
    }
}
