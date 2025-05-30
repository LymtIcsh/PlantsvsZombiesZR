using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunNumber : MonoBehaviour
{
    //阳光数文本
    Text myText;
    int nowSun;
    
    //卡槽群组
    List<Card> cardGroup;


    // Start is called before the first frame update
    void Start()
    {
        //获取阳光数
        myText = gameObject.GetComponent<Text>();

        if (GameManagement.levelData.StartSunNumber == -1)
        {
            if (GameManagement.levelData.isDay)
            { nowSun = 150; }
            else { nowSun = 50; }

          
        }
        else
        {
            nowSun = GameManagement.levelData.StartSunNumber;
        }

       

        myText.text = nowSun.ToString();


        //更新卡槽状态
        updateCard();
    }

    public void setCardGroup(List<Card> group)
    {
        cardGroup = group;
    }

    public void addSun(int sunNum)
    {
        nowSun += sunNum;
        if(nowSun >= 9990)
        {
            SetAchievement.SetAchievementCompleted("9990");
        }
        myText.text = nowSun.ToString();
        //更新卡槽状态
        updateCard();
    }

    public void subSun(int sunNum)
    {
        if(nowSun >= sunNum)
        {
            nowSun -= sunNum;
            myText.text = nowSun.ToString();
            //更新卡槽状态
            updateCard();
        }
    }

    private void updateCard()
    {
        foreach(Card i in cardGroup)
        {
            i.updateSunEnough(nowSun >= i.sunNeeded);
        }
    }

    public int GetSunNum()
    {//获取现在阳光值

        return nowSun;
    }
}
