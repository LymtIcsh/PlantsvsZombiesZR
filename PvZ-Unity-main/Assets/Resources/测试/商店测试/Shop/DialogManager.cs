using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    // �Ի�������

    public GameObject DialogSpeech;//�Ի���
    public Text DialongText;//�Ի����UI�ַ���
    private Coroutine CloseCoroutine;//���ڹرնԻ���Ч����Э��



    void Start()
    {
        DialogSpeech.gameObject.SetActive(true);
        DialongText.text = "��ӭ�����������Ƶ�С��";
    }

    public void AddString(string String) { //��ӡ�ַ������Ի�����,���Ҵ�ӡ���5���رնԻ�
        print("�Ի�");
        DialogSpeech.gameObject.SetActive(true);
        DialongText.text = String;
        CloseSpeech();
    }



    public virtual void CloseSpeech()//���ڹرնԻ���Э�̵Ĵ���
    {
        // �����ǰ�����ڽ��е�Э�̣���ֹͣ��
        if (CloseCoroutine != null)
        {
            StopCoroutine(CloseCoroutine);
        }
        // �����µ�״̬Ч��Э��
        CloseCoroutine = StartCoroutine(ToCloseCoroutine());
    }
    private IEnumerator ToCloseCoroutine()
    {

        // �ȴ�5��
        yield return new WaitForSeconds(5);
        Restart();
    }

    void Restart()
    {//�Զ��رնԻ���
        DialogSpeech.gameObject.SetActive(false);
        DialongText.text = null;
    }

}
