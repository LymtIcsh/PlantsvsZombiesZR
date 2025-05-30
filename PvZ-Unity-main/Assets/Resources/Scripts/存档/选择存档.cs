using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class 选择存档 : MonoBehaviour
{
    public TMP_Dropdown saveDropdown;
    public Button loadSaveButton;
    public Button 删除存档;
    private string currentSaveName = "Player";
    public GameObject 选择账户;
    public TMP_Text 显示用户名;
    public 冒险模式 冒险模式;

    void Start()
    {
        currentSaveName = LevelManagerStatic.GetCurrentSaveName();
        显示用户名.text = currentSaveName;
        InitializeDropdown();
        loadSaveButton.onClick.AddListener(OnLoadSaveButtonClicked);
        删除存档.onClick.AddListener(点击删除存档按钮);
        选择账户.SetActive(false);
    }

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

    public void OnLoadSaveButtonClicked()
    {
        string selectedSaveName = saveDropdown.options[saveDropdown.value].text;
        LevelManagerStatic.CreateOrLoadSaveFile(selectedSaveName);
        currentSaveName = selectedSaveName;
        显示用户名.text = currentSaveName;
        冒险模式.CheckForFirstUncompletedLevel();
        隐藏();
    }

    public void 重载显示的存档()
    {
        string selectedSaveName = saveDropdown.options[saveDropdown.value].text;
        LevelManagerStatic.CreateOrLoadSaveFile(selectedSaveName);
        currentSaveName = selectedSaveName;
        显示用户名.text = currentSaveName;
        冒险模式.CheckForFirstUncompletedLevel();
    }

    public void 显示()
    {
        选择账户.SetActive(true);
        StaticThingsManagement.打开二级界面 = true;
    }

    public void 隐藏()
    {
        选择账户.SetActive(false);
        StaticThingsManagement.打开二级界面 = false;
    }

    public void 点击删除存档按钮()
    {
        GetComponent<删除存档>().删除存档名 = saveDropdown.options[saveDropdown.value].text;
        GetComponent<删除存档>().显示删除存档界面();
    }
}
