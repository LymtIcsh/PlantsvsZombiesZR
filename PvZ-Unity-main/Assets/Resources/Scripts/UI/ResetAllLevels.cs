using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAllLevels : MonoBehaviour
{
    public void ResetAllLevelsToWin()
    {
        LevelManagerStatic.ResetAllLevelsToWin();
        SceneManager.LoadScene
            (SceneManager.GetActiveScene().name);
    }

    public void ResetAllLevelsNotToWin()
    {
        LevelManagerStatic.ResetAllLevelsToNotWin();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
