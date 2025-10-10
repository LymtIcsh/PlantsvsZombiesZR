using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 选择存档 - 管理存档选择功能
/// </summary>
public class SelectSaveFile : MonoBehaviour
{
    /// <summary>
    /// saveDropdown - 存档选择下拉菜单
    /// </summary>
    public TMP_Dropdown saveDropdown;

    /// <summary>
    /// loadSaveButton - 加载存档按钮
    /// </summary>
    public Button loadSaveButton;

    /// <summary>
    /// 删除存档 - 删除存档按钮
    /// </summary>
    public Button DeleteSaveButton;

    private string currentSaveName = "Player";

    /// <summary>
    /// 选择账户 - 选择账户的UI面板
    /// </summary>
    public GameObject SelectAccountPanel;

    /// <summary>
    /// 显示用户名 - 显示当前用户名的文本
    /// </summary>
    public TMP_Text DisplayUsername;

    /// <summary>
    /// 冒险模式 - 冒险模式组件引用
    /// </summary>
    public AdventureMode AdventureModeComponent;

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
