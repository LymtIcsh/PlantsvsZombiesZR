using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 删除存档 - 管理存档删除功能
/// </summary>
public class DeleteSaveFile : MonoBehaviour
{
    /// <summary>
    /// 删除存档界面 - 删除确认的UI面板
    /// </summary>
    public GameObject DeletePanel;

    /// <summary>
    /// 删除存档名 - 要删除的存档名称
    /// </summary>
    public string SaveNameToDelete;

    /// <summary>
    /// 存档名显示 - 显示存档名称的文本
    /// </summary>
    public TMP_Text SaveNameDisplay;

    /// <summary>
    /// 确定 - 确认删除按钮
    /// </summary>
    public Button ConfirmButton;

    /// <summary>
    /// 取消 - 取消删除按钮
    /// </summary>
    public Button CancelButton;

    /// <summary>
    /// 冒险模式 - 冒险模式组件引用
    /// </summary>
    public AdventureMode AdventureModeComponent;

    void Start()
    {
        DeletePanel.SetActive(false);
    }

    /// <summary>
    /// 显示删除存档界面 - 显示删除确认面板
    /// </summary>
    public void ShowDeletePanel()
    {
        DeletePanel.gameObject.SetActive(true);
        SaveNameDisplay.text = SaveNameToDelete;
    }

    /// <summary>
    /// 删除 - 执行删除存档操作
    /// </summary>
    public void Delete()
    {
        LevelManagerStatic.DeleteUserSaveFile(SaveNameToDelete);
        AdventureModeComponent.CheckForFirstUncompletedLevel();
        GetComponent<SelectSaveFile>().InitializeDropdown();
        GetComponent<SelectSaveFile>().ReloadDisplayedSave();
        DeletePanel.SetActive(false);
    }

    /// <summary>
    /// 取消删除 - 取消删除操作
    /// </summary>
    public void CancelDelete()
    {
        DeletePanel.SetActive(false);
    }
}
