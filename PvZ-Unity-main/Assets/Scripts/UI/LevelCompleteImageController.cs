using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI; // ��Ҫ���� UI

public class LevelCompleteImageController : MonoBehaviour
{
    public int levelToCheck; // Ҫ���Ĺؿ���
    private Image levelCompleteImage;
    public bool mustShow;//�������̶���ʾֲ����۹ؿ��Ƿ�ͨ��

    void Start()
    {
        // ��ȡ��ǰ�����ϵ� Image ���
        levelCompleteImage = GetComponent<Image>();

        // ���ؿ��Ƿ�ͨ��
        CheckLevelCompletion();
    }

    void CheckLevelCompletion()
    {
        // ���� LevelManager ��̬��ķ��������ùؿ��Ƿ���ͨ��
        if (LevelManagerStatic.IsLevelCompleted(levelToCheck))
        {
            // ���ͨ�أ���ʾͼ��
            levelCompleteImage.enabled = true;
            
            
        }
        else
        {
            // ��������ͼ��
            levelCompleteImage.enabled = false;
            
        }
        if(mustShow)
        {

            levelCompleteImage.enabled = true;
        }
    }
}
