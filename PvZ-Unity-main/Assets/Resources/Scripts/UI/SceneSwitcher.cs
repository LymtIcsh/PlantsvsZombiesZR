using UnityEngine;
using UnityEngine.SceneManagement; // ���볡�����������ռ�
using UnityEngine.UI; // ����UI�����ռ�

public class SceneSwitcher : MonoBehaviour
{
    // ����������Ա���ť�ĵ���¼�����
    public void SwitchScene(string sceneName)
    {
        Time.timeScale = 1;
        // �����µĳ���
        SceneManager.LoadScene(sceneName);
    }
}

