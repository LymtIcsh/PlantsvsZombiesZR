using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    // 金钱管理器

    public int Money;//有多少钱
    public Text MoneyText;
    void Start()
    {
        MoneyText.text = Money.ToString();
    }

    public void AddMoney(int Count) {//加钱
        Money += Count;
        MoneyText.text = Money.ToString();
    }

    public void SubMoney(int Count) {
        if (Money >= Count)
        {
            Money -= Count;
            MoneyText.text = Money.ToString();
        }
        else {
            print("牢弟你的钱不够");
        }
    }
}
