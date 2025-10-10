using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// 兑换码
/// </summary>
public class RedemptionCodeManager : MonoBehaviour
{
    public TMP_InputField inputField;
    [FormerlySerializedAs("确定")] [Header("确定")]
    public Button confirmButton;
    private List<RedemptionCode> redemptionCodes = new List<RedemptionCode>();

    private void Awake()
    {
        //redemptionCodes.Add(new RedemptionCode(301, "dev", 开启开发者));
    }

    public void Start()
    {
        confirmButton.onClick.AddListener(ConfirmInput);
    }
/// <summary>
/// 确认输入
/// </summary>
    public void ConfirmInput()
    {
        string Text = inputField.text;

        if(Text == "dev")
        {
            EnableDeveloperMode();
            inputField.text = "成功开启开发者模式";
            return;
        }

        RedemptionCode redemptionCode = redemptionCodes.Find(rc => rc.Code == Text);
        if (redemptionCode != null && !LevelManagerStatic.IsLevelCompleted(redemptionCode.ID))
        {
            Debug.Log("兑换成功");
            inputField.text = "兑换成功！";
            LevelManagerStatic.SetLevelCompleted(redemptionCode.ID);
            redemptionCode.RedeemEvent.Invoke();
        }
        else
        {
            inputField.text = "兑换码无效或已经兑换!";
            Debug.Log("兑换码无效或已经兑换!");
        }
        Debug.Log(Text);
    }

/// <summary>
/// 显示自身
/// </summary>
    public void ShowSelf()
    {
        gameObject.SetActive(true);
        StaticThingsManagement.IsSecondaryPanelOpen = true;
    }
/// <summary>
/// 隐藏自身
/// </summary>
    public void HideSelf()
    {
        gameObject.SetActive(false);
        StaticThingsManagement.IsSecondaryPanelOpen = false;
    }

/// <summary>
/// 开启开发者
/// </summary>
    public void EnableDeveloperMode()
    {
        dev.DeveloperMode = true;
    }

}

public class RedemptionCode
{
    public int ID { get; set; }
    public string Code { get; set; }
    public Action RedeemEvent { get; set; }

    public RedemptionCode(int id, string code, Action redeemEvent)
    {
        ID = id;
        Code = code;
        RedeemEvent = redeemEvent;
    }
}