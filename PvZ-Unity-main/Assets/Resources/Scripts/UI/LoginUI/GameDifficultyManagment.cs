using UnityEngine;
using UnityEngine.UI;

public class GameDifficultyManagement : MonoBehaviour
{
    public Slider difficultySlider;  // ���������
    public Text difficultyText;  // ��ʾ��ǰ�Ѷȵ��ı�����ѡ��

    private void Start()
    {
        // ��ʼ������������������ֵ�仯
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        // ���û������ĳ�ʼֵΪ��ǰ��GameDifficult
        difficultySlider.value = GameManagement.GameDifficult;
        UpdateDifficultyText();
    }

    // ����������ֵ�仯ʱ���ô˺���
    private void OnDifficultyChanged(float value)
    {
        GameManagement.GameDifficult = value;  // ����������ֵ����GameDifficult
        UpdateDifficultyText();
    }

    // ����UI����ʾ�ĵ�ǰ�Ѷ�ֵ����ѡ��
    private void UpdateDifficultyText()
    {
    }
}

