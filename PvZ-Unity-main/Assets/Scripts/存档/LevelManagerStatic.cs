using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LevelManagerStatic
{
    public static bool enviornmentPlant = true;

    private static string archiveFolderPath = Path.Combine(Application.persistentDataPath, "Archive");
    private static string currentSaveFilePath = Path.Combine(archiveFolderPath, "currentSave.txt");
    private static string currentSaveName = "Player";

    public static List<LevelInfo> levels = new List<LevelInfo>();
    public static List<string> chooseCards = new List<string>();

    static LevelManagerStatic()
    {
        // 确保 Archive 文件夹存在
        if (!Directory.Exists(archiveFolderPath))
        {
            Directory.CreateDirectory(archiveFolderPath);
        }

        LoadCurrentSaveName();
        LoadLevelStatus();
    }

    public static void LoadCurrentSaveName()
    {
        Debug.Log("Loading current save name from: " + currentSaveFilePath);

        if (File.Exists(currentSaveFilePath))
        {
            currentSaveName = File.ReadAllText(currentSaveFilePath).Trim();
            Debug.Log("Loaded current save name: " + currentSaveName);
        }
        else
        {
            Debug.Log("Current save file not found. Creating default save file.");
            SaveCurrentSaveName();
        }
    }

    public static void SaveCurrentSaveName()
    {
        Debug.Log("Saving current save name to: " + currentSaveFilePath);

        // 确保 Archive 文件夹存在
        if (!Directory.Exists(archiveFolderPath))
        {
            Directory.CreateDirectory(archiveFolderPath);
        }

        File.WriteAllText(currentSaveFilePath, currentSaveName);
    }

    public static string GetCurrentSaveName()
    {
        return currentSaveName;
    }

    public static void CreateOrLoadSaveFile(string saveName)
    {
        currentSaveName = saveName;
        SaveCurrentSaveName();

        string saveFolderPath = Path.Combine(archiveFolderPath, saveName);

        if (Directory.Exists(saveFolderPath))
        {
            Debug.Log("Loading existing save folder: " + saveFolderPath);
            LoadLevelStatus();
        }
        else
        {
            Debug.Log("Creating new save folder: " + saveFolderPath);
            Directory.CreateDirectory(saveFolderPath);
            InitializeDefaultLevelStatus();
        }
    }

    public static void SaveLevelStatus()
    {
        string saveFilePath = Path.Combine(archiveFolderPath, currentSaveName, "levelStatus.json");

        // 确保保存文件夹存在
        if (!Directory.Exists(Path.Combine(archiveFolderPath, currentSaveName)))
        {
            Directory.CreateDirectory(Path.Combine(archiveFolderPath, currentSaveName));
        }

        string json = JsonUtility.ToJson(new LevelInfoList(levels), true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Level status saved to: " + saveFilePath);
    }

    public static void LoadLevelStatus()
    {
        string saveFilePath = Path.Combine(archiveFolderPath, currentSaveName, "levelStatus.json");

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            LevelInfoList levelList = JsonUtility.FromJson<LevelInfoList>(json);
            levels = levelList.levels;
            Debug.Log("Level status loaded from: " + saveFilePath);
        }
        else
        {
            Debug.Log("Level status file not found. Initializing default level status.");
            InitializeDefaultLevelStatus();
        }
    }

    private static void InitializeDefaultLevelStatus()
    {
        levels.Clear();
        for (int i = 1; i <= 550; i++)
        {
            levels.Add(new LevelInfo(i, false));
        }
        SaveLevelStatus();
        Debug.Log("Default level status initialized.");
    }

    public static void SetLevelCompleted(int levelNumber)
    {
        LevelInfo level = levels.Find(l => l.levelNumber == levelNumber);
        if (level != null)
        {
            level.isCompleted = true;
            SaveLevelStatus();
            Debug.Log("Level " + levelNumber + " marked as completed.");
        }
    }

    public static void ResetAllLevelsToNotWin()
    {
        for (int i = 0; i < 300; i++)
        {
            levels[i].isCompleted = false;
        }
        SaveLevelStatus();
        Debug.Log("All levels reset to not completed.");
    }

    public static void ResetAllLevelsToWin()
    {
        for (int i = 0; i < 300; i++)
        {
            levels[i].isCompleted = true;
        }
        SaveLevelStatus();
        Debug.Log("All levels reset to completed.");
    }

    public static bool IsLevelCompleted(int levelNumber)
    {
        LevelInfo level = levels.Find(l => l.levelNumber == levelNumber);

        if (level != null)
        {
            return level.isCompleted;
        }

        return false;
    }

    public static void DeleteUserSaveFile(string username)
    {
        string saveFolderPath = Path.Combine(archiveFolderPath, username);

        if (Directory.Exists(saveFolderPath))
        {
            if (username == currentSaveName)
            {
                string[] saveFiles = Directory.GetDirectories(archiveFolderPath);
                List<string> saveNames = new List<string>();

                foreach (string saveFile in saveFiles)
                {
                    saveNames.Add(Path.GetFileName(saveFile));
                }

                if (saveNames.Count > 1)
                {
                    Directory.Delete(saveFolderPath, true);
                    saveNames.Remove(username);

                    string newSaveName = saveNames[0];
                    CreateOrLoadSaveFile(newSaveName);
                    Debug.Log("Deleted current save folder and switched to: " + newSaveName);
                }
                else
                {
                    Debug.Log("Cannot delete the only remaining save folder.");
                    return;
                }
            }
            else
            {
                Directory.Delete(saveFolderPath, true);
                Debug.Log("Deleted save folder: " + saveFolderPath);
            }
        }
    }
}

[System.Serializable]
public class LevelInfo
{
    public int levelNumber;
    public bool isCompleted;

    public LevelInfo() { }

    public LevelInfo(int levelNumber, bool isCompleted)
    {
        this.levelNumber = levelNumber;
        this.isCompleted = isCompleted;
    }
}

[System.Serializable]
public class LevelInfoList
{
    public List<LevelInfo> levels;
    public LevelInfoList(List<LevelInfo> levels)
    {
        this.levels = levels;
    }
}