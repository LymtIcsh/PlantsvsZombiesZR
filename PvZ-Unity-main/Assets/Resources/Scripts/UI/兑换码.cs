using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class 兑换码 : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button 确定;
    private List<RedemptionCode> redemptionCodes = new List<RedemptionCode>();

    private void Awake()
    {
        //redemptionCodes.Add(new RedemptionCode(301, "dev", 开启开发者));
    }

    public void Start()
    {
        确定.onClick.AddListener(确认输入);
    }

    public void 确认输入()
    {
        string Text = inputField.text;

        if(Text == "dev")
        {
            开启开发者();
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

    public void 显示自身()
    {
        gameObject.SetActive(true);
        StaticThingsManagement.打开二级界面 = true;
    }

    public void 隐藏自身()
    {
        gameObject.SetActive(false);
        StaticThingsManagement.打开二级界面 = false;
    }

    public void 开启开发者()
    {
        dev.开发者模式 = true;
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