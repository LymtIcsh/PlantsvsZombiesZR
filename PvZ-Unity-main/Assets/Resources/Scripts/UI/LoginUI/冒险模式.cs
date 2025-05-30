using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class 冒险模式 : MonoBehaviour
{
    public Collider2DButton 冒险交互;
    public Image 冒险显示;
    public Text 大关文字;
    public Sprite 开始冒险;
    public Sprite 开始冒险高亮;
    public Sprite 冒险;
    public Sprite 冒险高亮;
    public GameObject 僵尸手;
    public Animator animator;
    public float 间隔时间 = 2f;//用于计算两个音效空余时间
    bool allLevelsCompleted = true; //记录是否全部关卡通关

    public List<int> 检测关卡 = new List<int> { 1 , 2 , 3 , 4 , 5 , 6 };
    public void Awake()
    {
        animator.enabled = false;
    }
    public void Start()
    {
        CheckForFirstUncompletedLevel();
        僵尸手.SetActive(false);
    }

    public void 点击事件调用()
    {
        

        关卡返回代码.游戏模式 = 游戏模式.冒险模式;

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
                开始LevelDialog1();
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

    public void 开始LevelDialog1()
    {
        僵尸手.SetActive(true);
        animator.enabled = true;
        StartCoroutine(LevelDialog1控制());
    }

    private IEnumerator LevelDialog1控制()
    {
        AudioManager.Instance.PlaySoundEffect(37);
        yield return new WaitForSeconds(间隔时间);
        AudioManager.Instance.PlaySoundEffect(38);
        yield return new WaitForSeconds(2.5f);
        BeginManagement.level = 1;
        SceneManager.LoadScene("GameScene");
        yield return null;
    }


    public void CheckForFirstUncompletedLevel()
    {
        allLevelsCompleted = true;
        foreach (int i in 检测关卡)
        {
            LevelInfo level = LevelManagerStatic.levels[i - 1];
            if (!level.isCompleted)
            {
                allLevelsCompleted = false;
                BeginManagement.level = level.levelNumber;
                Debug.Log(level.levelNumber);

                if (level.levelNumber == 1)
                {
                    LevelDialog1专用函数();
                }
                else
                {
                    正常关卡显示(level.levelNumber);
                }
                return;
            }
        }

        if (allLevelsCompleted)
        {
            全部通关显示();
        }
    }

    private void LevelDialog1专用函数()
    {
        冒险显示.sprite = 开始冒险;
        冒险交互.normalSprite = 开始冒险;
        冒险交互.highlightedSprite = 开始冒险高亮;
        Debug.Log("开始冒险");
        大关文字.gameObject.SetActive(false);
    }

    private void 正常关卡显示(int level)
    {
        冒险显示.sprite = 冒险;
        冒险交互.highlightedSprite = 冒险高亮;
        大关文字.gameObject.SetActive(true);
        大关文字.text = "第 " + level + " 关";
    }

    private void 全部通关显示()
    {
        冒险显示.sprite = 冒险;
        冒险交互.highlightedSprite = 冒险高亮;
        大关文字.gameObject.SetActive(true);
        大关文字.text = "自由选关";
    }
}
