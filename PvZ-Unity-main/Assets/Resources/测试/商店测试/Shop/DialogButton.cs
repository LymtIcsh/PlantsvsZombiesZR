using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogButton : MonoBehaviour
{
    //商店对话按钮
    public DialogManager dialogManager;
    private string myString;//要填入对话框中的字符串
   
    public void OnMouseDown()
    {
        int Type = Random.Range(0, 15);
        switch (Type)
        {
            case 0: myString = "你嫌贵？不要乱说好吗，这么多年来都是这个价格"; break;
            case 1:
                myString = "对标融合版？我们可没有LPP团队那样的技术力和时间（悲）";break;
            case 2:
                myString = "做改版最怕的不是动画难画或者是代码难写，\n" +
                    "最怕的是凉啊"; break;
            case 3: myString = "多多支持群系版，我什么都会做的（卑微）"; break;
            case 4: myString = "有些东西是免费的，因为我已经帮你付过钱了"; break;
            case 5: myString = "戴夫不在，因为他疯了"; break;
            case 6: myString = "其实我的真实身份是......生姜老师！"; break;
            case 7: myString = "这是待会要用到的神秘妙妙工具"; break;
            case 8: myString = "就凭你也想把我们的游戏玩闪退？"; break;
            case 9: myString = "打不过关卡？菜就多练，牢弟"; break;
            case 10: myString = "我们的项目会开源，助力热爱PVZ的人们"; break;
            case 11: myString = "小店还在装修，暂不支持购买商品哦"; break;

            default: myString = "欢迎游玩群系版"; break;

        }
        print("聊天");
        dialogManager.AddString(myString);

    }
     

}
