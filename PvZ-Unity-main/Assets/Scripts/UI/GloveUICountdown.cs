using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
/// <summary>
/// 手套UI倒计时
/// </summary>
public class GloveUICountdown : MonoBehaviour
{
    // 公共变量，供 Inspector 使用
    [Header("触发冷却的物体（例如Glove）")]
    public GameObject glove;        
    [FormerlySerializedAs("倒计时时长")] [Header("冷却时长，单位为秒")]
    public float countdownDuration = 5f;   
    [FormerlySerializedAs("倒计时遮罩")] [Header("显示倒计时的遮罩物体")]
    public GameObject countdownMask;
    [FormerlySerializedAs("能否使用")] [Header("能否使用的物体（例如按钮）")]
    public GameObject usabilityIndicator;    

  /// <summary>
  /// 倒计时遮罩的 RectTransform
  /// </summary>
    private RectTransform countdownMaskRectTransform;  
    /// <summary>
    /// 剩余的倒计时时间
    /// </summary>
    private float RemainingCountdown;       
    [FormerlySerializedAs("冷却中")] [Header("是否正在冷却中")]
    public bool isCoolingDown = false;      

    void Start()
    {
        // 初始化：确保物体开始时处于可用状态，倒计时遮罩隐藏
        countdownMaskRectTransform = countdownMask.GetComponent<RectTransform>();
        usabilityIndicator.SetActive(false);  // 开始时能否使用物体不可用
        countdownMask.SetActive(false); // 隐藏倒计时遮罩
    }

    /// <summary>
    /// 触发冷却的事件方法
    /// </summary>
    public void StartCooldown()
    {
        if(GameManagement.levelData.GloveHaveNoCD)
        {
            return;
        }

        if (!isCoolingDown)
        {
            isCoolingDown = true;
            RemainingCountdown = countdownDuration;

            // 激活倒计时遮罩和能否使用物体
            countdownMask.SetActive(true);
            usabilityIndicator.SetActive(true);

            StartCoroutine(UpdateCountdown()); // 启动协程更新倒计时
        }
    }

    /// <summary>
    /// 倒计时更新的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateCountdown()
    {
        //初始高度
        float initialHeight = 60f; // 初始高度为 60
        //当前高度
        float currentHeight = initialHeight;

        while (RemainingCountdown > 0)
        {
            RemainingCountdown -= Time.deltaTime;

            // 计算剩余时间与初始高度的比例
            currentHeight = Mathf.Lerp(0f, initialHeight, RemainingCountdown / countdownDuration);

            // 更新倒计时遮罩的高度
            countdownMaskRectTransform.sizeDelta = new Vector2(countdownMaskRectTransform.sizeDelta.x, currentHeight);

            yield return null; // 等待下一帧
        }

        // 倒计时结束，恢复初始状态
        ResetCooldown();
    }

    /// <summary>
    /// 冷却结束后的重置方法
    /// </summary>
    private void ResetCooldown()
    {
        usabilityIndicator.SetActive(false); // 禁用能否使用物体
        countdownMask.SetActive(false); // 隐藏倒计时遮罩
        
        isCoolingDown = false; // 结束冷却
    }
}
