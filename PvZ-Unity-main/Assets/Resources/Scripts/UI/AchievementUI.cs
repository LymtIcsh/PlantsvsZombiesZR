using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public static AchievementUI Instance;

    public GameObject specifiedAchievementPrefab;
    public GameObject achievementPrefab;  // 生成的成就模板（包含两个 Text）
    public Transform contentPanel;        // 用于放置生成的成就物体的容器（ScrollView 的 Content）
    public float yOffset = -562;         // 每个成就物体的垂直间距（可以根据需要调整）

    private float currentYPosition = 0f; // 当前物体生成的 y 坐标

    public Sprite sprite;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 确保只存在一个实例
        }
        GenerateAchievements();
    }

    public void GenerateAchievements()
    {
        // 检查 contentPanel 和 achievementPrefab 是否有效
        if (contentPanel == null)
        {
            Debug.LogError("Content Panel is not assigned!");
            return;
        }

        if (achievementPrefab == null)
        {
            Debug.LogError("Achievement Prefab is not assigned!");
            return;
        }
        // 清空当前成就列表
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);  // 清空已有的成就UI
        }

        // 获取 contentPanel 和 achievementPrefab 的 RectTransform
        RectTransform contentRectTransform = contentPanel.GetComponent<RectTransform>();
        RectTransform achievementRectTransform = achievementPrefab.GetComponent<RectTransform>();

        

        // 根据成就数量调整 contentPanel 的高度
        int totalAchievements = AchievementManager.achievements.Length;
        float totalHeight = totalAchievements * Mathf.Abs(yOffset);  // 计算总高度
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, totalHeight);  // 设置新的高度

        // 计算第一个物体的位置（相对坐标）
        float contentHeight = contentRectTransform.rect.height;
        float achievementHeight = achievementRectTransform.rect.height;

        // 第一个物体的 Y 坐标应该是 contentPanel 高度的一半加上物体高度的一半
        currentYPosition = (contentHeight / 2) - (achievementHeight) - 57;

        // 遍历所有成就并生成对应的 UI
        foreach (Achievement achievement in AchievementManager.achievements)
        {
            // 生成成就物体
            GameObject achievementObject = Instantiate(achievementPrefab, contentPanel);

            // 获取成就名和成就介绍的 Text 组件（通过子物体名称）
            Text achievementNameText = achievementObject.transform.Find("成就名")?.GetComponent<Text>();
            Text achievementDescriptionText = achievementObject.transform.Find("成就介绍")?.GetComponent<Text>();

            if (achievementNameText != null && achievementDescriptionText != null)
            {
                // 设置成就名和成就描述
                achievementNameText.text = achievement.name;
                achievementDescriptionText.text = achievement.description;

                // 根据成就是否完成设置颜色
                if (achievement.isCompleted)
                {
                    achievementNameText.color = Color.yellow;  // 完成的成就显示黄色
                }
                else
                {
                    achievementNameText.color = Color.gray;  // 未完成的成就显示灰色
                }
            }
            else
            {
                Debug.LogError("Text components for Achievement Name or Description are missing!");
            }

            // 调整物体的位置，按顺序从上到下排列
            RectTransform achievementRect = achievementObject.GetComponent<RectTransform>();
            achievementRect.anchoredPosition = new Vector2(0, currentYPosition);

            // 更新当前的 y 坐标，准备生成下一个成就
            currentYPosition -= Mathf.Abs(yOffset);  // 减少 Y 坐标，确保从上到下生成
        }
    }

    // 更新成就UI，标记成就为已完成并更新颜色
    public void UpdateAchievementUI(Achievement achievement)
    {
        // 在界面中查找这个成就对应的 Text
        foreach (Transform child in contentPanel)
        {
            Text[] texts = child.GetComponentsInChildren<Text>();
            if (texts[0].text == achievement.name)
            {
                // 如果成就完成，设置颜色为黄色
                texts[0].color = Color.yellow;
                break;
            }
        }
    }
}
