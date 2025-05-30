using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Security.Permissions;
using System.Collections;
using System;
public class CommodityManager : MonoBehaviour
{
    //商品管理
   
    public List<GameObject> Pages = new List<GameObject>();//商品的页
    private int nowPage;//现在的商品页码号 
    public List<Button> CarButtons = new List<Button>();//车上的两个按钮
    public DialogManager dialogManager;
    public MoneyManager moneyManager;
    private Commodity nowCommodity; //现在要购买的商品

    private Coroutine ClearCoroutine;//用于清除商品的协程

    public Animator 卡车动画;

    private void Start()
    {
        nowCommodity = null;
        nowPage = 0;
        for (int i = 0; i < Pages.Count; i++)//初始在第一页
        {
            if (nowPage == i)
            {
                Pages[i].SetActive(true);
            }
            else
            {
                Pages[i].SetActive(false);

            }
        }
    }

    public void ChangePage(bool Type) 
    {
        卡车动画.Play("CarDoor");
        
        //翻页,false为上一页，true为下一页
        if (Type == false)
        {
            nowPage--;
        }
        else {
            nowPage++;
        }

        nowPage = nowPage % Pages.Count;//防止过界
        if (nowPage < 0) { nowPage = Pages.Count-1; }
        

        for (int i = 0; i < Pages.Count; i++)//初始在第一页
        {
            if (nowPage == i)
            {
                Pages[i].SetActive(true);
            }
            else
            {
                Pages[i].SetActive(false);

            }
        }




    }

    public void AddStringToDialogManager(string myString,Commodity commodity) {//将商品的字符串赋值给对话管理器
        dialogManager.AddString(myString);
        print("描述商品");

        if (nowCommodity == null || nowCommodity!=commodity) { nowCommodity = commodity; }
        else { Buy();   }
        ClearCommdity();
    }

    public void Buy(string myString, Commodity commodity)
    {
        Buy();
        ClearCommdity();
    }

    private void Buy() { //玩家的购买操作
        if (nowCommodity == null) return;

        if (moneyManager.Money >= nowCommodity.Cost)
        {
            dialogManager.AddString("成交！");
            moneyManager.Money -= nowCommodity.Cost;
        }
        else {
            dialogManager.AddString("老弟你的钱呢？");

        }
    }

    //清除标记的商品

    public virtual void ClearCommdity()//用于清除商品协程的创建
    {
        // 如果当前有正在进行的协程，则停止它
        if (ClearCoroutine != null)
        {
            StopCoroutine(ClearCoroutine);
        }
        // 启动新的状态效果协程
        ClearCoroutine = StartCoroutine(ToCloseCoroutine());
    }
    private IEnumerator ToCloseCoroutine()
    {

        // 等待5秒
        yield return new WaitForSeconds(2);
        Restart();
    }

    void Restart()
    {
        nowCommodity = null;
    }

}

