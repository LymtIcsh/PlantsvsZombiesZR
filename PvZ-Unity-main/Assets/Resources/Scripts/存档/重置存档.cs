using UnityEngine;
using UnityEngine.SceneManagement;

public class 重置存档 : MonoBehaviour
{
    public GameObject 重置界面;

    private void Start()
    {
        重置界面.SetActive(false);
    }

    public void 取消()
    {
        重置界面.SetActive(false);
    }

    public void 显示()
    {
        重置界面.SetActive(true);
    }

    public void 确定()
    {
        LevelManagerStatic.ResetAllLevelsToNotWin();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
        重置界面.SetActive(false);
    }
}
