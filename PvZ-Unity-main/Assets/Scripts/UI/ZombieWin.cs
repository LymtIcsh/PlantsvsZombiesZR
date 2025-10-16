using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieWin : MonoBehaviour
{
    public float shakeAmount = 0.1f; // 抖动幅度
    public float shakeSpeed = 35f;    // 抖动频率

    private Vector3 originalPosition; // 物体的原始位置
    private float shakeTime;          // 抖动的计时器


    void Update()
    {
        shakeTime += Time.unscaledDeltaTime * shakeSpeed;

        // 使用正弦波生成周期性的偏移值
        float offsetX = Mathf.Sin(shakeTime) * shakeAmount;
        float offsetY = Mathf.Cos(shakeTime) * shakeAmount;

        // 将偏移值加到物体的原始位置
        transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
    }

    public ManagedAudioSource backgroundAudio;   //背景音乐的播放组件

    public void Start()
    {
        Time.timeScale = 0;
        //播放音效
        backgroundAudio.Stop();
        AudioManager.Instance.PlaySoundEffect(37);
        originalPosition = transform.position; // 获取物体的初始位置
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
