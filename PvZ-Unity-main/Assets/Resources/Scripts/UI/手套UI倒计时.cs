using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveUI倒计时 : MonoBehaviour
{
    // 公共变量，供 Inspector 使用
    public GameObject glove;         // 触发冷却的物体（例如Glove）
    public float 倒计时时长 = 5f;   // 冷却时长，单位为秒
    public GameObject 倒计时遮罩;   // 显示倒计时的遮罩物体
    public GameObject 能否使用;     // 能否使用的物体（例如按钮）

    private RectTransform 倒计时遮罩RectTransform;  // 倒计时遮罩的 RectTransform
    private float 剩余倒计时;        // 剩余的倒计时时间
    public bool 冷却中 = false;      // 是否正在冷却中

    void Start()
    {
        // 初始化：确保物体开始时处于可用状态，倒计时遮罩隐藏
        倒计时遮罩RectTransform = 倒计时遮罩.GetComponent<RectTransform>();
        能否使用.SetActive(false);  // 开始时能否使用物体不可用
        倒计时遮罩.SetActive(false); // 隐藏倒计时遮罩
    }

    // 触发冷却的事件方法
    public void 启动冷却()
    {
        if(GameManagement.levelData.GloveHaveNoCD)
        {
            return;
        }

        if (!冷却中)
        {
            冷却中 = true;
            剩余倒计时 = 倒计时时长;

            // 激活倒计时遮罩和能否使用物体
            倒计时遮罩.SetActive(true);
            能否使用.SetActive(true);

            StartCoroutine(更新倒计时()); // 启动协程更新倒计时
        }
    }

    // 倒计时更新的协程
    private IEnumerator 更新倒计时()
    {
        float 初始高度 = 60f; // 初始高度为 60
        float 当前高度 = 初始高度;

        while (剩余倒计时 > 0)
        {
            剩余倒计时 -= Time.deltaTime;

            // 计算剩余时间与初始高度的比例
            当前高度 = Mathf.Lerp(0f, 初始高度, 剩余倒计时 / 倒计时时长);

            // 更新倒计时遮罩的高度
            倒计时遮罩RectTransform.sizeDelta = new Vector2(倒计时遮罩RectTransform.sizeDelta.x, 当前高度);

            yield return null; // 等待下一帧
        }

        // 倒计时结束，恢复初始状态
        重置冷却();
    }

    // 冷却结束后的重置方法
    private void 重置冷却()
    {
        能否使用.SetActive(false); // 禁用能否使用物体
        倒计时遮罩.SetActive(false); // 隐藏倒计时遮罩
        
        冷却中 = false; // 结束冷却
    }
}
