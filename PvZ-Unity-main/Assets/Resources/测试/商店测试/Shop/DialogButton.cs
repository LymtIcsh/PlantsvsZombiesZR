using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogButton : MonoBehaviour
{
    //�̵�Ի���ť
    public DialogManager dialogManager;
    private string myString;//Ҫ����Ի����е��ַ���
   
    public void OnMouseDown()
    {
        int Type = Random.Range(0, 15);
        switch (Type)
        {
            case 0: myString = "���ӹ󣿲�Ҫ��˵������ô��������������۸�"; break;
            case 1:
                myString = "�Ա��ںϰ棿���ǿ�û��LPP�Ŷ������ļ�������ʱ�䣨����";break;
            case 2:
                myString = "���İ����µĲ��Ƕ����ѻ������Ǵ�����д��\n" +
                    "���µ�������"; break;
            case 3: myString = "���֧��Ⱥϵ�棬��ʲô�������ģ���΢��"; break;
            case 4: myString = "��Щ��������ѵģ���Ϊ���Ѿ����㸶��Ǯ��"; break;
            case 5: myString = "�����ڣ���Ϊ������"; break;
            case 6: myString = "��ʵ�ҵ���ʵ�����......������ʦ��"; break;
            case 7: myString = "���Ǵ���Ҫ�õ������������"; break;
            case 8: myString = "��ƾ��Ҳ������ǵ���Ϸ�����ˣ�"; break;
            case 9: myString = "�򲻹��ؿ����˾Ͷ������ε�"; break;
            case 10: myString = "���ǵ���Ŀ�ῪԴ�������Ȱ�PVZ������"; break;
            case 11: myString = "С�껹��װ�ޣ��ݲ�֧�ֹ�����ƷŶ"; break;

            default: myString = "��ӭ����Ⱥϵ��"; break;

        }
        print("����");
        dialogManager.AddString(myString);

    }
     

}
