using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 冒险模式
/// </summary>
public class AdventureMode : MonoBehaviour
{
    [FormerlySerializedAs("冒险交互")] [Header("冒险交互")]
    public Collider2DButton adventureInteraction;
    [FormerlySerializedAs("冒险显示")] [Header("冒险显示")]
    public Image adventureDisplay;
    [FormerlySerializedAs("大关文字")] [Header("大关文字")]
    public Text levelText;
    [FormerlySerializedAs("开始冒险")] [Header("开始冒险")]
    public Sprite startAdventureSprite;
    [FormerlySerializedAs("开始冒险高亮")] [Header("开始冒险高亮")]
    public Sprite startAdventureHighlightSprite;
    [FormerlySerializedAs("冒险")] [Header("冒险")]
    public Sprite adventureSprite;
    [FormerlySerializedAs("冒险高亮")] [Header("冒险高亮")]
    public Sprite adventureHighlightSprite;
    [FormerlySerializedAs("僵尸手")] [Header("僵尸手")]
    public GameObject zombieHand;
    public Animator animator; 
    [FormerlySerializedAs("间隔时间")] [Header("间隔时间")]
    public float intervalTime = 2f;//用于计算两个音效空余时间
    bool allLevelsCompleted = true; //记录是否全部关卡通关
    [FormerlySerializedAs("检测关卡")] [Header("检测关卡")]
    public List<int> levelsToCheck = new List<int> { 1 , 2 , 3 , 4 , 5 , 6 };
    public void Awake()
    {
        animator.enabled = false;
    }
    public void Start()
    {
        CheckForFirstUncompletedLevel();
        zombieHand.SetActive(false);
    }

    /// <summary>
    /// 点击事件调用
    /// </summary>
    public void OnClickEvent()
    {


        LevelReturnCode.CurrentGameMode = GameMode.AdventureMode;

        if (!allLevelsCompleted)
        {
            Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

            foreach (Button button in buttons)
            {
                button.interactable = false;
            }

            Collider2DButton[] colliderButtons = FindObjectsByType<Collider2DButton>(FindObjectsSortMode.None);

            foreach (Collider2DButton collider2DButton in colliderButtons)
            {
                collider2DButton.enabled = false;
            }

            if (BeginManagement.level == 1)
            {
                StartLevelDialog1();
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            SceneManager.LoadScene("ChooseGame");
        }
        
    }

    /// <summary>
    /// 开始LevelDialog1
    /// </summary>
    public void StartLevelDialog1()
    {
        zombieHand.SetActive(true);
        animator.enabled = true;
        StartCoroutine(LevelDialog1Control());
    }

    /// <summary>
    /// LevelDialog1控制
    /// </summary>
    /// <returns></returns>
    private IEnumerator LevelDialog1Control()
    {
        AudioManager.Instance.PlaySoundEffect(37);
        yield return new WaitForSeconds(intervalTime);
        AudioManager.Instance.PlaySoundEffect(38);
        yield return new WaitForSeconds(2.5f);
        BeginManagement.level = 1;
        SceneManager.LoadScene("GameScene");
        yield return null;
    }


    public void CheckForFirstUncompletedLevel()
    {
        allLevelsCompleted = true;
        foreach (int i in levelsToCheck)
        {
            LevelInfo level = LevelManagerStatic.levels[i - 1];
            if (!level.isCompleted)
            {
                allLevelsCompleted = false;
                BeginManagement.level = level.levelNumber;
                Debug.Log(level.levelNumber);

                if (level.levelNumber == 1)
                {
                    LevelDialog1SpecialFunction();
                }
                else
                {
                    ShowNormalLevel(level.levelNumber);
                }
                return;
            }
        }

        if (allLevelsCompleted)
        {
            ShowAllLevelsCompleted();
        }
    }

    /// <summary>
    /// LevelDialog1专用函数
    /// </summary>
    private void LevelDialog1SpecialFunction()
    {
        adventureDisplay.sprite = startAdventureSprite;
        adventureInteraction.normalSprite = startAdventureSprite;
        adventureInteraction.highlightedSprite = startAdventureHighlightSprite;
        Debug.Log("开始冒险");
        levelText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 正常关卡显示
    /// </summary>
    /// <param name="level"> 关卡数</param>
    private void ShowNormalLevel(int level)
    {
        adventureDisplay.sprite = adventureSprite;
        adventureInteraction.highlightedSprite = adventureHighlightSprite;
        levelText.gameObject.SetActive(true);
        levelText.text = "第 " + level + " 关";
    }

    /// <summary>
    /// 全部通关显示
    /// </summary>
    private void ShowAllLevelsCompleted()
    {
        adventureDisplay.sprite = adventureSprite;
        adventureInteraction.highlightedSprite = adventureHighlightSprite;
        levelText.gameObject.SetActive(true);
        levelText.text = "自由选关";
    }
}
