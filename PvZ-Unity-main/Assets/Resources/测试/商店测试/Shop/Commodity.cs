using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commodity : MonoBehaviour
{
    // 商品
    public int Cost;
    public Text CostText;
    public String myString;//商品描述
    public CommodityManager commodityManager;
    void Start()
    {
        CostText.text = Cost.ToString();//花费赋值到UI中
    }
    public void OnMouseDown()
    {
        commodityManager.AddStringToDialogManager(myString,this);
    }

}
