using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 货币管理器
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) clickCoin();
    }

    private void clickCoin()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] allSun = Physics2D.OverlapPointAll(mouseWorldPos, LayerMask.GetMask("货币"));
        if (allSun.Length > 0)
        {
            Debug.Log("点击");
            //allSun[allSun.Length - 1].gameObject.GetComponent<SunBase>().bePickedUp();
        }
    }
}
