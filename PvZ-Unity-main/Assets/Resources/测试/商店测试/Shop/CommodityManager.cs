using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Security.Permissions;
using System.Collections;
using System;
public class CommodityManager : MonoBehaviour
{
    //��Ʒ����
   
    public List<GameObject> Pages = new List<GameObject>();//��Ʒ��ҳ
    private int nowPage;//���ڵ���Ʒҳ��� 
    public List<Button> CarButtons = new List<Button>();//���ϵ�������ť
    public DialogManager dialogManager;
    public MoneyManager moneyManager;
    private Commodity nowCommodity; //����Ҫ�������Ʒ

    private Coroutine ClearCoroutine;//���������Ʒ��Э��

    public Animator ��������;

    private void Start()
    {
        nowCommodity = null;
        nowPage = 0;
        for (int i = 0; i < Pages.Count; i++)//��ʼ�ڵ�һҳ
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
        ��������.Play("CarDoor");
        
        //��ҳ,falseΪ��һҳ��trueΪ��һҳ
        if (Type == false)
        {
            nowPage--;
        }
        else {
            nowPage++;
        }

        nowPage = nowPage % Pages.Count;//��ֹ����
        if (nowPage < 0) { nowPage = Pages.Count-1; }
        

        for (int i = 0; i < Pages.Count; i++)//��ʼ�ڵ�һҳ
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

    public void AddStringToDialogManager(string myString,Commodity commodity) {//����Ʒ���ַ�����ֵ���Ի�������
        dialogManager.AddString(myString);
        print("������Ʒ");

        if (nowCommodity == null || nowCommodity!=commodity) { nowCommodity = commodity; }
        else { Buy();   }
        ClearCommdity();
    }

    public void Buy(string myString, Commodity commodity)
    {
        Buy();
        ClearCommdity();
    }

    private void Buy() { //��ҵĹ������
        if (nowCommodity == null) return;

        if (moneyManager.Money >= nowCommodity.Cost)
        {
            dialogManager.AddString("�ɽ���");
            moneyManager.Money -= nowCommodity.Cost;
        }
        else {
            dialogManager.AddString("�ϵ����Ǯ�أ�");

        }
    }

    //�����ǵ���Ʒ

    public virtual void ClearCommdity()//���������ƷЭ�̵Ĵ���
    {
        // �����ǰ�����ڽ��е�Э�̣���ֹͣ��
        if (ClearCoroutine != null)
        {
            StopCoroutine(ClearCoroutine);
        }
        // �����µ�״̬Ч��Э��
        ClearCoroutine = StartCoroutine(ToCloseCoroutine());
    }
    private IEnumerator ToCloseCoroutine()
    {

        // �ȴ�5��
        yield return new WaitForSeconds(2);
        Restart();
    }

    void Restart()
    {
        nowCommodity = null;
    }

}

