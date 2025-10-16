using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool isCompleted;
}

[System.Serializable]
public class AchievementList
{
    public Achievement[] achievements;
}

public class AchievementManager : MonoBehaviour
{
    public static Achievement[] achievements;
    private static string filePath;
    void Awake()
    {
        filePath = Application.persistentDataPath + "/achievements.json";  // �����ļ�·��
        // ���سɾ�����
        LoadAchievements();
    }

    

    // ��̬���������ڱ�ǳɾ����
    public static void CompleteAchievement(string achievementName)
    {
        // �ҵ���Ӧ�ĳɾ�
        Achievement achievement = System.Array.Find(achievements, a => a.name == achievementName);

        if (achievement != null && !achievement.isCompleted)
        {
            // ����Ϊ���
            achievement.isCompleted = true;


            // ����ɾ�����
            SaveAchievements();
        }
    }

    // ����ɾ����ݵ� JSON �ļ�
    public static void SaveAchievements()
    {
        AchievementList achievementList = new AchievementList();
        achievementList.achievements = achievements;

        string json = JsonUtility.ToJson(achievementList, true);  // ת��Ϊ JSON �ַ���
        File.WriteAllText(filePath, json);  // ���浽�ļ�
        AchievementUI achievementUI = FindFirstObjectByType<AchievementUI>();
        if(achievementUI != null)
        {
            achievementUI.GenerateAchievements();
        }
    }

    // �� JSON �ļ����سɾ�����
    public static void LoadAchievements()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);  // ��ȡ�ļ�����
            AchievementList achievementList = JsonUtility.FromJson<AchievementList>(json);  // ���� JSON

            achievements = achievementList.achievements;

            // ����Ƿ��������ɾͣ���������ӵ���ǰ�ɾ�����
            CheckAndAddMissingAchievements();
        }
        else
        {
            // ����ļ������ڣ���ʼ��Ĭ�ϳɾ�
            achievements = new Achievement[]
            {
                new Achievement { name = "�ٻ���š��ټ�����", description = "������Ϸ", isCompleted = false },
                new Achievement { name = "�ο�ʼ�ĵط���", description = "����ؿ�1 - 1", isCompleted = false },
                //new Achievement { name = "�����治�������", description = "����˳���ť", isCompleted = false },
                //new Achievement { name = "лл", description = "�鿴����������", isCompleted = false },
                new Achievement { name = "��ֲ����ô�氡��", description = "�鿴ֲ��ͼ��", isCompleted = false },
                new Achievement { name = "9990", description = "������Ϸ���۳���9990������", isCompleted = false },
                new Achievement { name = "ʱ�����ã�TRJ��", description = "����ʱ������", isCompleted = false },
                new Achievement { name = "�������ֶ�����", description = "ʹ�ó�Ƭ", isCompleted = false },
                //new Achievement { name = "���գ�ɭ�ֻ��סһ��", description = "�鿴ɭ�ּ���", isCompleted = false },
                //new Achievement { name = "ɭ�ּ��䡤��֣�", description = "���ܽ�ʬ��ʿ-ɭ��", isCompleted = false },
                new Achievement { name = "�Ҳ��Ƕ���", description = "Ϊ������ʬ���ӳ���60�㶾��", isCompleted = false },
                //new Achievement { name = "�� �� ��", description = "����1.0��ʽ�漰��ǰ�汾����Ϸ", isCompleted = false },
                //new Achievement { name = "����������У��Ҳ���������У�", description = "��������п����������", isCompleted = false },
                new Achievement { name = "ֲ���ս��ʬ�˻���", description = "�ñ������տ�������տ�", isCompleted = false },
                //new Achievement { name = "�ҵ����ǲ���������", description = "���ǻ���˵��", isCompleted = false },
                //new Achievement { name = "ȼ�����ˣ�", description = "��ɭ���ݻ��յ�100���ݴ�", isCompleted = false },
                //new Achievement { name = "��׮�ս���", description = "��50��ɭ����׮", isCompleted = false },
                //new Achievement { name = "�������Ǳ�ը��", description = "����ɭ��ը��", isCompleted = false },
                //new Achievement { name = "�ҿ����ˡ�������Ϣ���ļ�����", description = "����ɭ�ּӳɣ�ɭ�ֵ�����Ϊ������������Ϣ�ķҷ���", isCompleted = false },
                //new Achievement { name = "ʦ�ĳ���������", description = "����һ��ֲ�ｩʬ���ӵ���", isCompleted = false },
                //new Achievement { name = "����������", description = "ʹ�÷����������ȡ��ʬ�ķ���", isCompleted = false },
            };

            // ����һ���µ� JSON �ļ�
            SaveAchievements();
        }
    }

    // ��鲢���ȱ�ٵĳɾ�
    private static void CheckAndAddMissingAchievements()
    {
        // ȷ�����гɾ͵��б����ԴӴ����л���������Դ��̬��ȡ��
        Achievement[] defaultAchievements = new Achievement[]
        {
           new Achievement { name = "�ٻ���š��ټ�����", description = "������Ϸ", isCompleted = false },
                new Achievement { name = "�ο�ʼ�ĵط���", description = "����ؿ�1 - 1", isCompleted = false },
                //new Achievement { name = "�����治�������", description = "����˳���ť", isCompleted = false },
                //new Achievement { name = "лл", description = "�鿴����������", isCompleted = false },
                new Achievement { name = "��ֲ����ô�氡��", description = "�鿴ֲ��ͼ��", isCompleted = false },
                new Achievement { name = "9990", description = "������Ϸ���۳���9990������", isCompleted = false },
                new Achievement { name = "ʱ�����ã�TRJ��", description = "����ʱ������", isCompleted = false },
                new Achievement { name = "�������ֶ�����", description = "ʹ�ó�Ƭ", isCompleted = false },
                //new Achievement { name = "���գ�ɭ�ֻ��סһ��", description = "�鿴ɭ�ּ���", isCompleted = false },
                //new Achievement { name = "ɭ�ּ��䡤��֣�", description = "���ܽ�ʬ��ʿ-ɭ��", isCompleted = false },
                new Achievement { name = "�Ҳ��Ƕ���", description = "Ϊ������ʬ���ӳ���60�㶾��", isCompleted = false },
                //new Achievement { name = "�� �� ��", description = "����1.0��ʽ�漰��ǰ�汾����Ϸ", isCompleted = false },
                //new Achievement { name = "����������У��Ҳ���������У�", description = "��������п����������", isCompleted = false },
                new Achievement { name = "ֲ���ս��ʬ�˻���", description = "�ñ������տ�������տ�", isCompleted = false },
                //new Achievement { name = "�ҵ����ǲ���������", description = "���ǻ���˵��", isCompleted = false },
                //new Achievement { name = "ȼ�����ˣ�", description = "��ɭ���ݻ��յ�100���ݴ�", isCompleted = false },
                //new Achievement { name = "��׮�ս���", description = "��50��ɭ����׮", isCompleted = false },
                //new Achievement { name = "�������Ǳ�ը��", description = "����ɭ��ը��", isCompleted = false },
                //new Achievement { name = "�ҿ����ˡ�������Ϣ���ļ�����", description = "����ɭ�ּӳɣ�ɭ�ֵ�����Ϊ������������Ϣ�ķҷ���", isCompleted = false },
                //new Achievement { name = "ʦ�ĳ���������", description = "����һ��ֲ�ｩʬ���ӵ���", isCompleted = false },
                //new Achievement { name = "����������", description = "ʹ�÷����������ȡ��ʬ�ķ���", isCompleted = false },
        };

        // ����Ĭ�ϳɾͣ���鵱ǰ�ɾ����Ƿ�����óɾͣ���û�������
        foreach (var defaultAchievement in defaultAchievements)
        {
            if (Array.Find(achievements, a => a.name == defaultAchievement.name) == null)
            {
                // ��ȱ�ٵĳɾ���ӵ�����
                Array.Resize(ref achievements, achievements.Length + 1);
                achievements[achievements.Length - 1] = defaultAchievement;
            }
        }

        // ���������µĳɾͣ�������º������
        SaveAchievements();
    }
}
