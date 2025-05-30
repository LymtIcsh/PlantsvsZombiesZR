using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseIlustratedTypeButton : MonoBehaviour
{
    public 图鉴模式 图鉴导出种类;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        关卡返回代码.图鉴模式 = 图鉴导出种类;
        SceneManager.LoadScene("Illustrated");
    }

}

