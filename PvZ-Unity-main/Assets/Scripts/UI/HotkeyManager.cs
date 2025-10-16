using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 快捷键
/// </summary>
public class HotkeyManager : MonoBehaviour
{
    public GameObject Shovel;
    public GameObject Glove;
    [FormerlySerializedAs("UI控制器")] [Header("UI控制器")]
    public GameObject UIController;
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
                Glove.GetComponent<Glove>().Cancel();
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
                Shovel.GetComponent<Shovel>().Cancel();
                Glove.GetComponent<Glove>().clickGlove();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            ToggleHealthDisplay();
            
        }
        // 检测是否按下Esc键
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.GetComponent<LoginUIManagement>().ToggleGameSettings();
        }
    }

    /// <summary>
    /// 切换血量显示
    /// </summary>
    public void ToggleHealthDisplay()
    {
        if (ZombieManagement.zombiesOnField.Count > 0 || PlantManagement.PlantsInFieldList.Count > 0)
        {
            GameManagement.isShowHp = !GameManagement.isShowHp;
            foreach (Zombie z in ZombieManagement.allZombies.ToArray())
            {
                if (z != null)
                {
                    z.ChangeHealthDisplay(GameManagement.isShowHp);
                }

            }
            for (int i = 0; i < PlantManagement.PlantsInFieldList.Count; i++)
            {
                GameObject p = PlantManagement.PlantsInFieldList[i];

                if (p != null)
                {
                    Plant ps = p.GetComponent<Plant>();
                    if (ps != null)
                    {
                        ps.ChangeBloodBolumeDisplay(GameManagement.isShowHp);
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
