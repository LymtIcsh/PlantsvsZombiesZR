using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    // 对话管理器

    public GameObject DialogSpeech;//对话框
    public Text DialongText;//对话框的UI字符串
    private Coroutine CloseCoroutine;//用于关闭对话框效果的协程



    void Start()
    {
        DialogSpeech.gameObject.SetActive(true);
        DialongText.text = "欢迎光临我氧化钙的小店";
    }

    public void AddString(string String) { //打印字符串到对话框中,并且打印后的5秒后关闭对话
        print("对话");
        DialogSpeech.gameObject.SetActive(true);
        DialongText.text = String;
        CloseSpeech();
    }



    public virtual void CloseSpeech()//用于关闭对话框协程的创建
    {
        // 如果当前有正在进行的协程，则停止它
        if (CloseCoroutine != null)
        {
            StopCoroutine(CloseCoroutine);
        }
        // 启动新的状态效果协程
        CloseCoroutine = StartCoroutine(ToCloseCoroutine());
    }
    private IEnumerator ToCloseCoroutine()
    {

        // 等待5秒
        yield return new WaitForSeconds(5);
        Restart();
    }

    void Restart()
    {//自动关闭对话框
        DialogSpeech.gameObject.SetActive(false);
        DialongText.text = null;
    }

}
