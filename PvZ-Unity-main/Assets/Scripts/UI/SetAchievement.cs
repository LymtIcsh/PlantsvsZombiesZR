using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class SetAchievement : MonoBehaviour
{
    private static SetAchievement _instance;

    public static SetAchievement Instance
    {
        get
        {
            if (_instance == null)
            {
                // ���������û���������Բ�����
                _instance = FindFirstObjectByType<SetAchievement>();
                if (_instance == null)
                {
                    // ����������Ҳ���������һ���µ� GameObject �����ش˽ű�
                    GameObject obj = new GameObject("AchievementCoroutineHandler");
                    _instance = obj.AddComponent<SetAchievement>();
                }
            }
            return _instance;
        }
    }
    private static bool isProcessing = false; // ����Ƿ����ڴ���ɾ�
    private static Queue<System.Action> achievementQueue = new Queue<System.Action>(); // �洢��ִ�еĳɾ�����

    public static void SetAchievementCompleted(string achievementName)
    {

        // ��������ӵ�������
        achievementQueue.Enqueue(() => CompleteAchievement(achievementName));

        // �����ǰû�����ڴ������񣬿�ʼ�������
        if (!isProcessing)
        {
            SetAchievement.Instance.StartCoroutine(ProcessQueue());
        }
    }


    // ��������е�����
    private static IEnumerator ProcessQueue()
    {
        isProcessing = true;

        // ��������е���������
        while (achievementQueue.Count > 0)
        {
            // ��ȡ�����е���һ������
            System.Action currentTask = achievementQueue.Dequeue();

            // ִ������
            currentTask.Invoke();

            // �ȴ� 5 �����ִ����һ������
            yield return new WaitForSeconds(1.1f);
        }

        // �������������
        isProcessing = false;
    }

    // ���ָ���ĳɾ�
    private static void CompleteAchievement(string achievementName)
    {
        if (AchievementManager.achievements == null || AchievementManager.achievements.Length == 0)
        {
            Debug.LogError("AchievementManager.achievements is null or empty. Make sure it's initialized before use.");
            return;
        }


        Achievement achievement = System.Array.Find(AchievementManager.achievements, a => a.name == achievementName);

        if (achievement != null && !achievement.isCompleted)
        {
            achievement.isCompleted = true;

            Debug.Log($"�ɾ� '{achievementName}' �����");

            // ��������ʾ�ɾ�����
            ShowAchievementDescription(achievementName);

            // ����ɾ�����
            AchievementManager.SaveAchievements();
        }
    }



    // �������гɾ�Ϊ�����
    public static void SetAllAchievementsCompleted()
    {
        // ȷ���ɾ��б�����ҷǿ�
        if (AchievementManager.achievements == null || AchievementManager.achievements.Length == 0)
        {
            Debug.LogError("AchievementManager.achievements is null or empty. Make sure it's initialized before use.");
            return;
        }

        foreach (var achievement in AchievementManager.achievements)
        {
            // Ϊÿ���ɾ͵�����ɷ���
            SetAchievementCompleted(achievement.name);
        }

        Debug.Log("���гɾ������");
        AchievementManager.SaveAchievements(); // ���浽 JSON �ļ�
    }


    // �������гɾ�Ϊδ���
    public static void SetAllAchievementsNotCompleted()
    {
        foreach (var achievement in AchievementManager.achievements)
        {
            if (achievement.isCompleted)
            {
                achievement.isCompleted = false;
            }
        }

        Debug.Log("���гɾ�������Ϊδ���");
        AchievementManager.SaveAchievements(); // ���浽 JSON �ļ�
    }



    public static void ShowAchievementDescription(string achievementName)
    {
        GameObject achievementObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ShowAchievement"));
        

        if (achievementObject != null)
        {
            // ��������
            achievementObject.SetActive(true);



            // ������Ϊ "�ɾ�����" ��������
            Transform descriptionTransform = FindChildByName(achievementObject.transform,"�ɾ�����");

            if (descriptionTransform != null)
            {
                // ��ȡ�������е� Text ���
                Text descriptionText = descriptionTransform.GetComponent<Text>();

                if (descriptionText != null)
                {
                    // ���óɾ������ı�Ϊָ������
                    descriptionText.text = achievementName;
                }
                else
                {
                    Debug.LogError("�ɾ�����������û���ҵ� Text ���");
                }
            }
            else
            {
                Debug.LogError("û���ҵ���Ϊ '�ɾ�����' ��������");
            }
        }
        else
        {
            Debug.LogError("û���ҵ���Ϊ 'ShowAchievement' ������");
        }
    }

    // �Զ��巽�������Ҳ���Ծ������
    private static GameObject FindInactiveObjectByName(string name)
    {
        // ���������е��������壨��������Ծ�ģ�
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    private static Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            // �ݹ����������
            Transform result = FindChildByName(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
