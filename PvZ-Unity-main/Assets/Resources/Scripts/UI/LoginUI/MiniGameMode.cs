using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 小游戏模式
/// </summary>
public class MiniGameMode : MonoBehaviour
{
    public void 被点击()
    {
        LevelReturnCode.CurrentGameMode = GameMode.MiniGameMode;
        SceneManager.LoadScene("ChooseGame");
    }
}
