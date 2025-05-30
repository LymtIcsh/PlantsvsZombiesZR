using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour ,IPointerEnterHandler
{
    private int buttonEnterSound; // 音效文件
    public int buttonType = 0;//0为悬停bleep音效，1为关卡按下click音效，2为卡片按下bleep音效
    private void Start()
    {
        // 获取 Button 组件
        Button button = GetComponent<Button>();
        if(buttonType != 1)
        {
            buttonEnterSound = 5;
        }
        else
        {
            buttonEnterSound = 4;
        }
        
        // 为 Button 添加点击事件监听器，点击时播放音效
        if (button != null && buttonType!=0)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    // 播放音效函数
    private void PlaySound()
    {
        AudioManager.Instance.PlaySoundEffect(buttonEnterSound);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(buttonType == 0)
        {
            PlaySound(); // 播放音效
        }
        
    }

}
