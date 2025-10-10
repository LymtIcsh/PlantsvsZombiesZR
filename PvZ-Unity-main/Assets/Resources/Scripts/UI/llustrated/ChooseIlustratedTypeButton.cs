using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseIlustratedTypeButton : MonoBehaviour
{
    /// <summary>
    /// 图鉴导出种类 - 指定要显示的图鉴类型
    /// </summary>
    public IllustratedMode IllustratedExportType;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        LevelReturnCode.CurrentIllustratedMode = IllustratedExportType;
        SceneManager.LoadScene("Illustrated");
    }

}

