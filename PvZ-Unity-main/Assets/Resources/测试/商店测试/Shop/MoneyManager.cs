using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    // ��Ǯ������

    public int Money;//�ж���Ǯ
    public Text MoneyText;
    void Start()
    {
        MoneyText.text = Money.ToString();
    }

    public void AddMoney(int Count) {//��Ǯ
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
            print("�ε����Ǯ����");
        }
    }
}
