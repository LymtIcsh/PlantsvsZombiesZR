using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Image fadeImage;  // 用于渐变的Image（黑色遮罩）
    public float fadeDuration = 1.0f;  // 渐变持续时间
    public float doubleTapTime = 0.3f;  // 允许双击的时间间隔

    private float lastTapTime = 0f;
    private bool isTransitioning = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime <= doubleTapTime)
            {
                // 双击触发
                isTransitioning = true;
                FadeToScene();
            }

            lastTapTime = currentTime;
        }
    }

    public void FadeToScene()
    {
        StartCoroutine(FadeOutAndLoadScene("Login"));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float timeElapsed = 0f;
        float startAlpha = fadeImage.color.a;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
