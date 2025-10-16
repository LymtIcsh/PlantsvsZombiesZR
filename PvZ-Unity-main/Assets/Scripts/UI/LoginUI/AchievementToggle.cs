using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 成就切换
/// </summary>
public class AchievementToggle : MonoBehaviour
{
    [FormerlySerializedAs("背景")] [Header("背景")]
    public GameObject background;//成就动画在background上
    public ScrollRect ScrollRect;
    public void Awake()
    {
        if(ScrollRect != null)
        {
            ScrollRect.verticalNormalizedPosition = 1F;
        }
        else
        {

        }
        
    }
    /// <summary>
    /// 切换至成就
    /// </summary>
    public void SwitchToAchievements()
    {
        ScrollRect.horizontal = true;  // 禁用水平滑动
        ScrollRect.vertical = true;    // 禁用垂直滑动
        background.GetComponent<Animator>().SetBool("成就", true);
    }

    /// <summary>
    /// 可点击按钮
    /// </summary>
    public void EnableButtons()
    {
        
        // 查找所有按钮
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        // 确保按钮数组不为空
        if (buttons != null && buttons.Length > 0)
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
        else
        {
            Debug.LogError("没有找到按钮！");
        }
    }

    /// <summary>
    /// 不可点击按钮
    /// </summary>
    public void DisableButtons()
    {
        
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// 退出成就
    /// </summary>
    public void ExitAchievements()
    {
        ScrollRect.horizontal = false;  // 禁用水平滑动
        ScrollRect.vertical = false;    // 禁用垂直滑动
        ScrollToTop(0.8f);
        background.GetComponent<Animator>().SetBool("成就", false);
    }


    /// <summary>
    /// 平滑回滚到顶部
    /// </summary>
    /// <param name="duration"></param>
    public void ScrollToTop(float duration)
    {
        StartCoroutine(SmoothScrollCoroutine(duration));
    }

    /// <summary>
    /// 平滑回滚Coroutine
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator SmoothScrollCoroutine(float duration)
    {
        float startPos = ScrollRect.verticalNormalizedPosition; // 初始位置
        float endPos = 1f; // 目标位置（顶部）
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            ScrollRect.verticalNormalizedPosition = Mathf.Lerp(startPos, endPos, timeElapsed / duration);
            yield return null;
        }

        // 确保最终位置为顶部（避免浮动误差）
        ScrollRect.verticalNormalizedPosition = endPos;
    }
}
