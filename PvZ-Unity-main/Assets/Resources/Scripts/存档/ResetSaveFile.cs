using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 重置存档 - 管理存档重置功能
/// </summary>
public class ResetSaveFile : MonoBehaviour
{
    /// <summary>
    /// 重置界面 - 重置确认的UI面板
    /// </summary>
    public GameObject ResetPanel;

    private void Start()
    {
        ResetPanel.SetActive(false);
    }

    /// <summary>
    /// 取消 - 取消重置操作
    /// </summary>
    public void Cancel()
    {
        ResetPanel.SetActive(false);
    }

    /// <summary>
    /// 显示 - 显示重置确认面板
    /// </summary>
    public void Show()
    {
        ResetPanel.SetActive(true);
    }

    /// <summary>
    /// 确定 - 确认重置存档
    /// </summary>
    public void Confirm()
    {
        LevelManagerStatic.ResetAllLevelsToNotWin();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
        ResetPanel.SetActive(false);
    }
}
