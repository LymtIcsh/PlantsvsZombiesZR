using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseIlustratedTypeButton : MonoBehaviour
{
    public ͼ��ģʽ ͼ����������;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        �ؿ����ش���.ͼ��ģʽ = ͼ����������;
        SceneManager.LoadScene("Illustrated");
    }

}

