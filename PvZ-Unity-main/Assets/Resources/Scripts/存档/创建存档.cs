using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class 创建存档 : MonoBehaviour
{
    public GameObject 创建界面;
    public Button 打开创建界面;
    public Button 创建;
    public Button 取消;
    public TMP_InputField inputField;

    private string archiveFolderPath;

    void Start()
    {
        archiveFolderPath = Path.Combine(Application.persistentDataPath, "Archive");
        EnsureArchiveFolderExists();
        打开创建界面.onClick.AddListener(OpenCreatePanel);
        创建.onClick.AddListener(CreateSaveFile);
        取消.onClick.AddListener(CloseCreatePanel);
        创建界面.SetActive(false);
    }

    private void EnsureArchiveFolderExists()
    {
        if (!Directory.Exists(archiveFolderPath))
        {
            Directory.CreateDirectory(archiveFolderPath);
        }
    }

    private void OpenCreatePanel()
    {
        创建界面.SetActive(true);
    }

    private void CloseCreatePanel()
    {
        创建界面.SetActive(false);
    }

    private void CreateSaveFile()
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
        GetComponent<选择存档>().InitializeDropdown();
    }
}
