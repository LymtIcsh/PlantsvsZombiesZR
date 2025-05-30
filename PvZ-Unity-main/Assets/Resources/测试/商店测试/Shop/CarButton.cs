using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CarButton : MonoBehaviour
{
    public bool Type;//false为向上翻页，True为向下翻页
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
