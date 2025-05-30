using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour ,IPointerEnterHandler
{
    private int buttonEnterSound; // ��Ч�ļ�
    public int buttonType = 0;//0Ϊ��ͣbleep��Ч��1Ϊ�ؿ�����click��Ч��2Ϊ��Ƭ����bleep��Ч
    private void Start()
    {
        // ��ȡ Button ���
        Button button = GetComponent<Button>();
        if(buttonType != 1)
        {
            buttonEnterSound = 5;
        }
        else
        {
            buttonEnterSound = 4;
        }
        
        // Ϊ Button ��ӵ���¼������������ʱ������Ч
        if (button != null && buttonType!=0)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    // ������Ч����
    private void PlaySound()
    {
        AudioManager.Instance.PlaySoundEffect(buttonEnterSound);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(buttonType == 0)
        {
            PlaySound(); // ������Ч
        }
        
    }

}
