using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseForestBuff : MonoBehaviour
{
    // 触发弹出指示框的按钮引用
    public Button triggerButton;

    // 指示框（弹出框）的引用
    public GameObject dialogBox;

    // 两个 Toggle 的引用
    public Toggle toggleOption1;
    public Toggle toggleOption2;

    // 确认按钮的引用
    public Button confirmButton;

    // 每个 Toggle 选择对应的事件
    public UnityEvent onToggle1Selected;
    public UnityEvent onToggle2Selected;

    // Start 是在第一次帧更新之前调用
    void Start()
    {
        // 初始时隐藏指示框
        dialogBox.SetActive(false);

        // 为按钮和 toggle 添加监听事件
        triggerButton.onClick.AddListener(OpenDialog);
        confirmButton.onClick.AddListener(ConfirmSelection);

        // 为 Toggle 添加监听器，确保选中时另一个 Toggle 被取消
        toggleOption1.onValueChanged.AddListener((value) => OnToggleValueChanged(value, toggleOption2));
        toggleOption2.onValueChanged.AddListener((value) => OnToggleValueChanged(value, toggleOption1));
    }

    // 打开指示框的函数，当触发按钮点击时调用
    void OpenDialog()
    {
        dialogBox.SetActive(true); // 显示指示框
        toggleOption1.isOn = false; // 打开指示框时重置 Toggle 状态
        toggleOption2.isOn = false;
    }

    // 确认按钮点击时的处理函数
    void ConfirmSelection()
    {
        if (toggleOption1.isOn)
        {
            Debug.Log("选择1");
            StaticThingsManagement.forestBuff = 0;
        }
        else if (toggleOption2.isOn)
        {
            Debug.Log("选择2");
            StaticThingsManagement.forestBuff = 1;
        }

        // 确认选择后关闭指示框
        dialogBox.SetActive(false);
    }

    // Toggle 选择变化时的处理函数，确保另一个 Toggle 取消选中
    void OnToggleValueChanged(bool value, Toggle otherToggle)
    {
        if (value) // 如果当前 Toggle 被选中
        {
            otherToggle.isOn = false; // 取消另一个 Toggle 的选中状态
        }
    }
}
