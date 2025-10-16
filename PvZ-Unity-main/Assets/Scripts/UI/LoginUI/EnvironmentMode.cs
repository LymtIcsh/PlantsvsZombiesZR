using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// 环境模式
/// </summary>
public class EnvironmentMode : MonoBehaviour
{
    [FormerlySerializedAs("遮罩")] [Header("遮罩")]
    public SpriteRenderer maskRenderer;
  
    /// <summary>
    /// 被点击
    /// </summary>
    public void OnClicked()
    {
        if(LevelManagerStatic.IsLevelCompleted(352))
        {
            LevelReturnCode.CurrentGameMode = GameMode.EnvironmentMode;
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
            StartCoroutine(FirstSceneTransition());
        }
        
    }

    /// <summary>
    /// 第一次转场
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstSceneTransition()
    {
        // float duration = 1.0f; // 持续时间 1 秒
        // float currentTime = 0f;
        //
        // // 初始颜色（Alpha 设置为 0）
        // Color startColor = maskRenderer.color;
        // startColor.a = 0f; // 初始透明度为 0
        // maskRenderer.color = startColor;
        //
        // while (currentTime < duration)
        // {
        //     currentTime += Time.deltaTime;
        //     float alpha = Mathf.Lerp(0f, 1f, currentTime / duration); // 从 0 渐变到 1
        //
        //     // 更新颜色
        //     maskRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        //     yield return null;
        // }
        //
        // // 确保最终 Alpha 为 1
        // maskRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        

        // 加载场景
        LevelReturnCode.CurrentGameMode = GameMode.EnvironmentMode;
        SceneManager.LoadScene("ChooseGame");
        yield return null;
    }
}
