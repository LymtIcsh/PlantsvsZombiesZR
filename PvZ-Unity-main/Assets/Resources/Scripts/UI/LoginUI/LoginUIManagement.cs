using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIManagement : MonoBehaviour
{
    public bool HasSpeedSlider = true;

    public GameObject gameSettings; // 拖拽引用 GameSettings UI 物体
    
    public Button buttonShow;       // 拖拽引用 显示按钮
    public Button buttonHide;       // 拖拽引用 隐藏按钮
    public Slider speedSlider; // 拖拽引用滑动条

    public Slider difficultySlider;  // 滑动条组件

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public Toggle collectSunToggle; // 在 Inspector 中拖放 Toggle 对象

    public Button OpenMakerList;
    public Button CloseMakerList;

    public bool hasmakerList;

    public Text GameDifficultyText;
    public Text GameDifficultyText2;

    public bool 可以设置性能优化模式;

    public Toggle 性能优化按钮;

    public Canvas targetCanvas;
    private bool isTopLayer = false;
    // 当 Toggle 状态变化时调用
    private void OnToggleValueChanged(bool isOn)
    {
        GameManagement.CollectSun = isOn;
    }


    // 当滑动条的值变化时调用此函数
    private void OnDifficultyChanged(float value)
    {
        GameManagement.GameDifficult = value;  // 将滑动条的值赋给GameDifficult
        GameDifficultyText.text = "难度 " + GameManagement.GameDifficult;
        if(GameDifficultyText2 != null)
        {
            GameDifficultyText2.text = "难度" + GameManagement.GameDifficult;
        }
    }

    // 当时间滑动条的值发生变化时调用该方法
    void OnSpeedSliderValueChanged(float value)
    {
        SetAchievement.SetAchievementCompleted("时轮逆旅（TRJ）");
    }

    private void OnPerformanceValueChanged(bool isOn)
    {
        GameManagement.isPerformance = isOn;
        Debug.Log(GameManagement.isPerformance);
    }

    void Start()
    {

        // 1. 先读取 AudioManager 中当前的值并赋给滑条
        musicVolumeSlider.value = AudioManager.Instance.musicVolume;
        sfxVolumeSlider.value = AudioManager.Instance.sfxVolume;

        // 2. 绑定回调：滑条改动时，调用 AudioManager 的接口
        musicVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        if (可以设置性能优化模式)
        {
            性能优化按钮.onValueChanged.AddListener(OnPerformanceValueChanged);
            性能优化按钮.isOn = GameManagement.isPerformance;
        }


        // 订阅 Toggle 的值变化事件
        collectSunToggle.onValueChanged.AddListener(OnToggleValueChanged);
        collectSunToggle.isOn = GameManagement.CollectSun;

        // 初始化滑动条，并监听数值变化
        difficultySlider.onValueChanged.AddListener(OnDifficultyChanged);
        // 设置滑动条的初始值为当前的GameDifficult
        difficultySlider.value = GameManagement.GameDifficult;
        GameDifficultyText.text = "难度 " + GameManagement.GameDifficult;
        if (GameDifficultyText2 != null)
        {
            GameDifficultyText2.text = "难度" + GameManagement.GameDifficult;
        }

        if (HasSpeedSlider)
        {
            // 将滑动条的初始值设置为当前游戏速度
            speedSlider.value = Time.timeScale;
            // 添加监听器，随时监控滑动条的值变化
            speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }
        
        gameSettings.SetActive(false);


        // 确保开始时 GameSettings 是不可见的
        gameSettings.SetActive(false);

        // 添加按钮点击事件监听器
        buttonShow.onClick.AddListener(ShowGameSettings);
        buttonHide.onClick.AddListener(HideGameSettings);



        
    }

    // 显示 GameSettings
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
        
        StaticThingsManagement.打开二级界面 = true;
        
        gameSettings.SetActive(true);
    }

    // 隐藏 GameSettings
    public void HideGameSettings()
    {
        if (HasSpeedSlider)
        {
            Time.timeScale = speedSlider.value;
            GameManagement.局内游戏速度 = speedSlider.value;
        }

        StaticThingsManagement.打开二级界面 = false;

        gameSettings.SetActive(false);
    }

    //检查 GameSettings 是否已显示并切换其显示状态
    public void ToggleGameSettings()
    {
        if(!buttonShow.isActiveAndEnabled)
        {
            return;
        }
        if (gameSettings.activeSelf)
        {
            HideGameSettings();  // 如果已显示，则隐藏
        }
        else
        {
            ShowGameSettings();  // 如果未显示，则显示
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
            // 切换 Canvas 的排序层级
            if (isTopLayer)
            {
                // 如果当前是在 TopUI 层级，切换回 Default
                targetCanvas.sortingLayerName = "BottomUI";
            }
            else
            {
                // 如果当前是在 Default 层级，切换到 TopUI
                targetCanvas.sortingLayerName = "TopUI";
            }

            // 切换状态
            isTopLayer = !isTopLayer;
        }
    }
}
