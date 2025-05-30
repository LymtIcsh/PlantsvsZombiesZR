using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 成就切换 : MonoBehaviour
{
    public GameObject 背景;//成就动画在background上
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
    public void 切换至成就()
    {
        ScrollRect.horizontal = true;  // 禁用水平滑动
        ScrollRect.vertical = true;    // 禁用垂直滑动
        背景.GetComponent<Animator>().SetBool("成就", true);
    }

    public void 可点击按钮()
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

    public void 不可点击按钮()
    {
        
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void 退出成就()
    {
        ScrollRect.horizontal = false;  // 禁用水平滑动
        ScrollRect.vertical = false;    // 禁用垂直滑动
        回滚至顶端(0.8f);
        背景.GetComponent<Animator>().SetBool("成就", false);
    }


    // 平滑回滚到顶部
    public void 回滚至顶端(float duration)
    {
        StartCoroutine(平滑回滚Coroutine(duration));
    }

    private IEnumerator 平滑回滚Coroutine(float duration)
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
