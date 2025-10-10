using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 创建存档 - 管理存档创建功能
/// </summary>
public class CreateSaveFile : MonoBehaviour
{
    /// <summary>
    /// 创建界面 - 创建存档的UI面板
    /// </summary>
    public GameObject CreatePanel;

    /// <summary>
    /// 打开创建界面 - 打开创建面板的按钮
    /// </summary>
    public Button OpenCreatePanelButton;

    /// <summary>
    /// 创建 - 确认创建存档的按钮
    /// </summary>
    public Button CreateButton;

    /// <summary>
    /// 取消 - 取消创建的按钮
    /// </summary>
    public Button CancelButton;

    /// <summary>
    /// inputField - 输入存档名称的输入框
    /// </summary>
    public TMP_InputField inputField;

    private string archiveFolderPath;

    void Start()
    {
        archiveFolderPath = Path.Combine(Application.persistentDataPath, "Archive");
        EnsureArchiveFolderExists();
        OpenCreatePanelButton.onClick.AddListener(OpenCreatePanel);
        CreateButton.onClick.AddListener(CreateSave);
        CancelButton.onClick.AddListener(CloseCreatePanel);
        CreatePanel.SetActive(false);
    }

    private void EnsureArchiveFolderExists()
    {
        if (!Directory.Exists(archiveFolderPath))
        {
            Directory.CreateDirectory(archiveFolderPath);
        }
    }

    /// <summary>
    /// OpenCreatePanel - 打开创建存档面板
    /// </summary>
    private void OpenCreatePanel()
    {
        CreatePanel.SetActive(true);
    }

    /// <summary>
    /// CloseCreatePanel - 关闭创建存档面板
    /// </summary>
    private void CloseCreatePanel()
    {
        CreatePanel.SetActive(false);
    }

    /// <summary>
    /// CreateSave - 创建新存档
    /// </summary>
    private void CreateSave()
    {
        string newSaveName = inputField.text.Trim();

        if (string.IsNullOrEmpty(newSaveName))
        {
            inputField.text = "请输入一个有效的存档名称！";
            return;
        }

        string newSaveFolderPath = Path.Combine(archiveFolderPath, newSaveName);
        if (Directory.Exists(newSaveFolderPath))
        {
            inputField.text = "该存档已存在！";
            return;
        }

        if (newSaveName.Length > 10)
        {
            inputField.text = "存档名称不能超过 10 个字符！";
            return;
        }

        Directory.CreateDirectory(newSaveFolderPath);
        LevelManagerStatic.CreateOrLoadSaveFile(newSaveName);
        CloseCreatePanel();
        GetComponent<SelectSaveFile>().InitializeDropdown();
    }
}
