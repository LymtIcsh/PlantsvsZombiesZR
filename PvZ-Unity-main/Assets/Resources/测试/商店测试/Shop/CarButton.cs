using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CarButton : MonoBehaviour
{
    public bool Type;//falseΪ���Ϸ�ҳ��TrueΪ���·�ҳ
    public CommodityManager commodityManager;

    public void OnMouseDown()
    {
        if (Type == false)
        {
            commodityManager.ChangePage(false);
        }
        else {
            commodityManager.ChangePage(true);
        }
    }


}
