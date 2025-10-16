using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieWin : MonoBehaviour
{
    public float shakeAmount = 0.1f; // ��������
    public float shakeSpeed = 35f;    // ����Ƶ��

    private Vector3 originalPosition; // �����ԭʼλ��
    private float shakeTime;          // �����ļ�ʱ��


    void Update()
    {
        shakeTime += Time.unscaledDeltaTime * shakeSpeed;

        // ʹ�����Ҳ����������Ե�ƫ��ֵ
        float offsetX = Mathf.Sin(shakeTime) * shakeAmount;
        float offsetY = Mathf.Cos(shakeTime) * shakeAmount;

        // ��ƫ��ֵ�ӵ������ԭʼλ��
        transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
    }

    public ManagedAudioSource backgroundAudio;   //�������ֵĲ������

    public void Start()
    {
        Time.timeScale = 0;
        //������Ч
        backgroundAudio.Stop();
        AudioManager.Instance.PlaySoundEffect(37);
        originalPosition = transform.position; // ��ȡ����ĳ�ʼλ��
    }

    public void Scream()
    {
        AudioManager.Instance.PlaySoundEffect(38);
    }

    public void exitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Login");
    }
}
