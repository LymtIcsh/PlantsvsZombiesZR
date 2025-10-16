using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseIlustratedTypeButton : MonoBehaviour
{
    /// <summary>
    /// ͼ���������� - ָ��Ҫ��ʾ��ͼ������
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

