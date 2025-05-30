using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 删除存档 : MonoBehaviour
{
    public GameObject 删除存档界面;
    public string 删除存档名;
    public TMP_Text 存档名显示;
    public Button 确定;
    public Button 取消;
    public 冒险模式 冒险模式;
    void Start()
    {
        删除存档界面.SetActive(false);
    }
    public void 显示删除存档界面()
    {
        删除存档界面.gameObject.SetActive(true);
        存档名显示.text = 删除存档名;
    }

    public void 删除()
    {
        LevelManagerStatic.DeleteUserSaveFile(删除存档名);
        冒险模式.CheckForFirstUncompletedLevel();
        GetComponent<选择存档>().InitializeDropdown();
        GetComponent<选择存档>().重载显示的存档();
        删除存档界面.SetActive(false);
    }

    public void 取消删除()
    {
        删除存档界面.SetActive(false);
    }
}
