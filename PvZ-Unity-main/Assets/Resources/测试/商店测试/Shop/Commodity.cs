using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commodity : MonoBehaviour
{
    // ��Ʒ
    public int Cost;
    public Text CostText;
    public String myString;//��Ʒ����
    public CommodityManager commodityManager;
    void Start()
    {
        CostText.text = Cost.ToString();//���Ѹ�ֵ��UI��
    }
    public void OnMouseDown()
    {
        commodityManager.AddStringToDialogManager(myString,this);
    }

}
