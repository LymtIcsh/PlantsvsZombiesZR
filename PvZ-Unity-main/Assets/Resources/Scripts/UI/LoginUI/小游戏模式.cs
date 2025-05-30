using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 小游戏模式 : MonoBehaviour
{
    public void 被点击()
    {
        关卡返回代码.游戏模式 = 游戏模式.小游戏模式;
        SceneManager.LoadScene("ChooseGame");
    }
}
