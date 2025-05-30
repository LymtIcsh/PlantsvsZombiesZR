using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIManagement : MonoBehaviour
{
    public bool HasSpeedSlider = true;

    public GameObject gameSettings; // ��ק���� GameSettings UI ����
    
    public Button buttonShow;       // ��ק���� ��ʾ��ť
    public Button buttonHide;       // ��ק���� ���ذ�ť
    public Slider speedSlider; // ��ק���û�����

    public Slider difficultySlider;  // ���������

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public Toggle collectSunToggle; // �� Inspector ���Ϸ� Toggle ����

    public Button OpenMakerList;
    public Button CloseMakerList;

    public bool hasmakerList;

    public Text GameDifficultyText;
    public Text GameDifficultyText2;

    public bool �������������Ż�ģʽ;

    public Toggle �����Ż���ť;

    public Canvas targetCanvas;
    private bool isTopLayer = false;
    // �� Toggle ״̬�仯ʱ����
    private void OnToggleValueChanged(bool isOn)
    {
        GameManagement.CollectSun = isOn;
    }


    // ����������ֵ�仯ʱ���ô˺���
    private void OnDifficultyChanged(float value)
    {
        GameManagement.GameDifficult = value;  // ����������ֵ����GameDifficult
        GameDifficultyText.text = "�Ѷ� " + GameManagement.GameDifficult;
        if(GameDifficultyText2 != null)
        {
            GameDifficultyText2.text = "�Ѷ�" + GameManagement.GameDifficult;
        }
    }

    // ��ʱ�们������ֵ�����仯ʱ���ø÷���
    void OnSpeedSliderValueChanged(float value)
    {
        SetAchievement.SetAchievementCompleted("ʱ�����ã�TRJ��");
    }

    private void OnPerformanceValueChanged(bool isOn)
    {
        GameManagement.isPerformance = isOn;
        Debug.Log(GameManagement.isPerformance);
    }

    void Start()
    {

        // 1. �ȶ�ȡ AudioManager �е�ǰ��ֵ����������
        musicVolumeSlider.value = AudioManager.Instance.musicVolume;
        sfxVolumeSlider.value = AudioManager.Instance.sfxVolume;

        // 2. �󶨻ص��������Ķ�ʱ������ AudioManager �Ľӿ�
        musicVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        if (�������������Ż�ģʽ)
        {
            �����Ż���ť.onValueChanged.AddListener(OnPerformanceValueChanged);
            �����Ż���ť.isOn = GameManagement.isPerformance;
        }


        // ���� Toggle ��ֵ�仯�¼�
        collectSunToggle.onValueChanged.AddListener(OnToggleValueChanged);
        collectSunToggle.isOn = GameManagement.CollectSun;

        // ��ʼ������������������ֵ�仯
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        // ���û������ĳ�ʼֵΪ��ǰ��GameDifficult
        difficultySlider.value = GameManagement.GameDifficult;
        GameDifficultyText.text = "�Ѷ� " + GameManagement.GameDifficult;
        if (GameDifficultyText2 != null)
        {
            GameDifficultyText2.text = "�Ѷ�" + GameManagement.GameDifficult;
        }

        if (HasSpeedSlider)
        {
            // ���������ĳ�ʼֵ����Ϊ��ǰ��Ϸ�ٶ�
            speedSlider.value = Time.timeScale;
            // ��Ӽ���������ʱ��ػ�������ֵ�仯
            speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }
        
        gameSettings.SetActive(false);


        // ȷ����ʼʱ GameSettings �ǲ��ɼ���
        gameSettings.SetActive(false);

        // ��Ӱ�ť����¼�������
        buttonShow.onClick.AddListener(ShowGameSettings);
        buttonHide.onClick.AddListener(HideGameSettings);



        
    }

    // ��ʾ GameSettings
    public void ShowGameSettings()
    {
        if (HasSpeedSlider)
        {
            Time.timeScale = 0;
        }

        musicVolumeSlider.value = AudioManager.Instance.musicVolume;
        sfxVolumeSlider.value = AudioManager.Instance.sfxVolume;

        collectSunToggle.isOn = GameManagement.CollectSun;
        difficultySlider.value = GameManagement.GameDifficult;
        
        StaticThingsManagement.�򿪶������� = true;
        
        gameSettings.SetActive(true);
    }

    // ���� GameSettings
    public void HideGameSettings()
    {
        if (HasSpeedSlider)
        {
            Time.timeScale = speedSlider.value;
            GameManagement.������Ϸ�ٶ� = speedSlider.value;
        }

        StaticThingsManagement.�򿪶������� = false;

        gameSettings.SetActive(false);
    }

    //��� GameSettings �Ƿ�����ʾ���л�����ʾ״̬
    public void ToggleGameSettings()
    {
        if(!buttonShow.isActiveAndEnabled)
        {
            return;
        }
        if (gameSettings.activeSelf)
        {
            HideGameSettings();  // �������ʾ��������
        }
        else
        {
            ShowGameSettings();  // ���δ��ʾ������ʾ
        }
    }



    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }

    public void ToggleCanvasLayer()
    {
        if (targetCanvas != null)
        {
            // �л� Canvas ������㼶
            if (isTopLayer)
            {
                // �����ǰ���� TopUI �㼶���л��� Default
                targetCanvas.sortingLayerName = "BottomUI";
            }
            else
            {
                // �����ǰ���� Default �㼶���л��� TopUI
                targetCanvas.sortingLayerName = "TopUI";
            }

            // �л�״̬
            isTopLayer = !isTopLayer;
        }
    }
}
