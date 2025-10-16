using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 小游戏模式
/// </summary>
public class MiniGameMode : MonoBehaviour
{
    /// <summary>
    /// 被点击
    /// </summary>
    public void OnClickEvent()
    {
        LevelReturnCode.CurrentGameMode = GameMode.MiniGameMode;
        SceneManager.LoadScene("ChooseGame");
    }
}
