using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManagement : MonoBehaviour
{
    public Slider speedSlider; // ��ק���û�����
    public Button Button;
    private float defaultTimeScale = 1f;
    private bool IsTimeout = false;

    void Start()
    {
        // ���������ĳ�ʼֵ����Ϊ��ǰ��Ϸ�ٶ�
        speedSlider.value = defaultTimeScale;

        // ��Ӽ���������ʱ��ػ�������ֵ�仯
        speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        Button.onClick.AddListener(OnTimeout);
    }

    // ����������ֵ�����仯ʱ���ø÷���
    void OnSpeedSliderValueChanged(float value)
    {
        // ������Ϸ��ʱ�����ţ��ı���Ϸ�ٶȣ�
        Time.timeScale = value;
    }

    void OnTimeout()
    {
        if (IsTimeout)
        {
            Time.timeScale = speedSlider.value;
            IsTimeout = false;
        }
        else
        {
            Time.timeScale = 0.1f;
            IsTimeout = true;
        }
    }

    void OnDestroy()
    {
        // �Ƴ�����������ѡ��
        speedSlider.onValueChanged.RemoveListener(OnSpeedSliderValueChanged);
    }
}
