using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public static AchievementUI Instance;

    public GameObject specifiedAchievementPrefab;
    public GameObject achievementPrefab;  // ���ɵĳɾ�ģ�壨�������� Text��
    public Transform contentPanel;        // ���ڷ������ɵĳɾ������������ScrollView �� Content��
    public float yOffset = -562;         // ÿ���ɾ�����Ĵ�ֱ��ࣨ���Ը�����Ҫ������

    private float currentYPosition = 0f; // ��ǰ�������ɵ� y ����

    public Sprite sprite;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // ȷ��ֻ����һ��ʵ��
        }
        GenerateAchievements();
    }

    public void GenerateAchievements()
    {
        // ��� contentPanel �� achievementPrefab �Ƿ���Ч
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
        // ��յ�ǰ�ɾ��б�
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);  // ������еĳɾ�UI
        }

        // ��ȡ contentPanel �� achievementPrefab �� RectTransform
        RectTransform contentRectTransform = contentPanel.GetComponent<RectTransform>();
        RectTransform achievementRectTransform = achievementPrefab.GetComponent<RectTransform>();

        

        // ���ݳɾ��������� contentPanel �ĸ߶�
        int totalAchievements = AchievementManager.achievements.Length;
        float totalHeight = totalAchievements * Mathf.Abs(yOffset);  // �����ܸ߶�
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, totalHeight);  // �����µĸ߶�

        // �����һ�������λ�ã�������꣩
        float contentHeight = contentRectTransform.rect.height;
        float achievementHeight = achievementRectTransform.rect.height;

        // ��һ������� Y ����Ӧ���� contentPanel �߶ȵ�һ���������߶ȵ�һ��
        currentYPosition = (contentHeight / 2) - (achievementHeight) - 57;

        // �������гɾͲ����ɶ�Ӧ�� UI
        foreach (Achievement achievement in AchievementManager.achievements)
        {
            // ���ɳɾ�����
            GameObject achievementObject = Instantiate(achievementPrefab, contentPanel);

            // ��ȡ�ɾ����ͳɾͽ��ܵ� Text �����ͨ�����������ƣ�
            Text achievementNameText = achievementObject.transform.Find("�ɾ���")?.GetComponent<Text>();
            Text achievementDescriptionText = achievementObject.transform.Find("�ɾͽ���")?.GetComponent<Text>();

            if (achievementNameText != null && achievementDescriptionText != null)
            {
                // ���óɾ����ͳɾ�����
                achievementNameText.text = achievement.name;
                achievementDescriptionText.text = achievement.description;

                // ���ݳɾ��Ƿ����������ɫ
                if (achievement.isCompleted)
                {
                    achievementNameText.color = Color.yellow;  // ��ɵĳɾ���ʾ��ɫ
                }
                else
                {
                    achievementNameText.color = Color.gray;  // δ��ɵĳɾ���ʾ��ɫ
                }
            }
            else
            {
                Debug.LogError("Text components for Achievement Name or Description are missing!");
            }

            // ���������λ�ã���˳����ϵ�������
            RectTransform achievementRect = achievementObject.GetComponent<RectTransform>();
            achievementRect.anchoredPosition = new Vector2(0, currentYPosition);

            // ���µ�ǰ�� y ���꣬׼��������һ���ɾ�
            currentYPosition -= Mathf.Abs(yOffset);  // ���� Y ���꣬ȷ�����ϵ�������
        }
    }

    // ���³ɾ�UI����ǳɾ�Ϊ����ɲ�������ɫ
    public void UpdateAchievementUI(Achievement achievement)
    {
        // �ڽ����в�������ɾͶ�Ӧ�� Text
        foreach (Transform child in contentPanel)
        {
            Text[] texts = child.GetComponentsInChildren<Text>();
            if (texts[0].text == achievement.name)
            {
                // ����ɾ���ɣ�������ɫΪ��ɫ
                texts[0].color = Color.yellow;
                break;
            }
        }
    }
}
