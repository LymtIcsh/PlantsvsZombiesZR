using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Serialization;

public class SelectSaveFile  : MonoBehaviour
{
    public TMP_Dropdown saveDropdown;
    public Button loadSaveButton;
    [FormerlySerializedAs("删除存档")] [Header("删除存档")]
    public Button DeleteSaveButton;
    private string currentSaveName = "Player";
    [FormerlySerializedAs("选择账户")] [Header("选择账户")]
    public GameObject SelectAccountPanel;
    [FormerlySerializedAs("显示用户名")] [Header("显示用户名")]
    public TMP_Text DisplayUsername;
    [FormerlySerializedAs("冒险模式")] [Header("冒险模式")]
    public AdventureMode  AdventureModeComponent;

  
    void Start()
    {
        currentSaveName = LevelManagerStatic.GetCurrentSaveName();
        DisplayUsername.text = currentSaveName;
        InitializeDropdown();
        loadSaveButton.onClick.AddListener(OnLoadSaveButtonClicked);
        DeleteSaveButton.onClick.AddListener(OnDeleteSaveButtonClicked);
        SelectAccountPanel.SetActive(false);
    }

    /// <summary>
    /// InitializeDropdown - 初始化存档下拉菜单
    /// </summary>
    public void InitializeDropdown()
    {
        string archiveFolderPath = Path.Combine(Application.persistentDataPath, "Archive");

        if (Directory.Exists(archiveFolderPath))
        {
            string[] saveFiles = Directory.GetDirectories(archiveFolderPath);
            List<string> saveNames = new List<string>();

            foreach (string saveFile in saveFiles)
            {
                saveNames.Add(Path.GetFileName(saveFile));
            }

            saveDropdown.ClearOptions();
            saveDropdown.AddOptions(saveNames);

            int defaultIndex = saveNames.IndexOf(currentSaveName);
            if (defaultIndex >= 0)
            {
                saveDropdown.value = defaultIndex;
            }
        }
    }

    /// <summary>
    /// OnLoadSaveButtonClicked - 加载存档按钮点击事件
    /// </summary>
    public void OnLoadSaveButtonClicked()
    {
        string selectedSaveName = saveDropdown.options[saveDropdown.value].text;
        LevelManagerStatic.CreateOrLoadSaveFile(selectedSaveName);
        currentSaveName = selectedSaveName;
        DisplayUsername.text = currentSaveName;
        AdventureModeComponent.CheckForFirstUncompletedLevel();
        Hide();
    }

    /// <summary>
    /// 重载显示的存档 - 重新加载并显示选中的存档
    /// </summary>
    public void ReloadDisplayedSave()
    {
        string selectedSaveName = saveDropdown.options[saveDropdown.value].text;
        LevelManagerStatic.CreateOrLoadSaveFile(selectedSaveName);
        currentSaveName = selectedSaveName;
        DisplayUsername.text = currentSaveName;
        AdventureModeComponent.CheckForFirstUncompletedLevel();
    }

    /// <summary>
    /// 显示 - 显示选择账户面板
    /// </summary>
    public void Show()
    {
        SelectAccountPanel.SetActive(true);
        StaticThingsManagement.IsSecondaryPanelOpen = true;
    }

    /// <summary>
    /// 隐藏 - 隐藏选择账户面板
    /// </summary>
    public void Hide()
    {
        SelectAccountPanel.SetActive(false);
        StaticThingsManagement.IsSecondaryPanelOpen = false;
    }

    /// <summary>
    /// 点击删除存档按钮 - 删除存档按钮点击事件
    /// </summary>
    public void OnDeleteSaveButtonClicked()
    {
        GetComponent<DeleteSaveFile>().SaveNameToDelete = saveDropdown.options[saveDropdown.value].text;
        GetComponent<DeleteSaveFile>().ShowDeletePanel();
    }
}

