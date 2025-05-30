using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 环境模式 : MonoBehaviour
{
    public SpriteRenderer 遮罩;
    public void 被点击()
    {
        if(LevelManagerStatic.IsLevelCompleted(352))
        {
            关卡返回代码.游戏模式 = 游戏模式.环境模式;
            SceneManager.LoadScene("ChooseGame");
        }
        else
        {
            Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);

            foreach (Canvas canvas in canvases)
            {
                CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
                }

                canvasGroup.interactable = false;
            }
            StartCoroutine(第一次转场());
        }
        
    }

    IEnumerator 第一次转场()
    {
        //float duration = 1.0f; // 持续时间 1 秒
        //float currentTime = 0f;

        //// 初始颜色（Alpha 设置为 0）
        //Color startColor = 遮罩.color;
        //startColor.a = 0f; // 初始透明度为 0
        //遮罩.color = startColor;

        //while (currentTime < duration)
        //{
        //    currentTime += Time.deltaTime;
        //    float alpha = Mathf.Lerp(0f, 1f, currentTime / duration); // 从 0 渐变到 1

        //    // 更新颜色
        //    遮罩.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        //    yield return null;
        //}

        //// 确保最终 Alpha 为 1
        //遮罩.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // 加载场景
        关卡返回代码.游戏模式 = 游戏模式.环境模式;
        SceneManager.LoadScene("ChooseGame");
        yield return null;
    }
}
