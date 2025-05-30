using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ManagedAudioSource))]
public class BGMusicControl : MonoBehaviour
{
    private Animator animator;
    private ManagedAudioSource managedAudio;
    public bool isClimax = false;
    private string musicName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        managedAudio = GetComponent<ManagedAudioSource>();

        animator.enabled = false;

        // 如果在编辑器中设置为在Awake时播放，自动执行播放
        if (managedAudio.playOnAwake && managedAudio.clip != null)
        {
            managedAudio.Play();
        }
    }

    /// <summary>
    /// 平滑切换音乐（淡出旧曲，淡入新曲）
    /// </summary>
    public void changeMusicSmoothly(string name)
    {
        musicName = name;
        StartCoroutine(FadeOutIn());
    }

    /// <summary>
    /// 立即切换音乐，不带淡入淡出
    /// </summary>
    public void changeMusic(string name = null)
    {
        if (!string.IsNullOrEmpty(name))
            musicName = name;

        AudioClip clip = Resources.Load<AudioClip>("Sounds/Background/" + musicName);
        if (clip == null)
        {
            Debug.LogWarning($"未找到音乐文件: {musicName}");
            return;
        }

        AudioManager.Instance.StopAllSceneMusic();

        managedAudio.clip = clip;
        managedAudio.loop = true;
        managedAudio.Play();
    }

    /// <summary>
    /// 禁用 Animator
    /// </summary>
    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    private IEnumerator FadeOutIn()
    {
        //float duration = 0.5f;
        //float startVolume = managedAudio.Source.volume;

        //// 淡出
        //float t = 0f;
        //while (t < duration)
        //{
        //    managedAudio.Source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
        //    t += Time.deltaTime;
        //    yield return null;
        //}
        //managedAudio.Source.volume = 0f;
        yield return null;
        // 切换音乐
        changeMusic();

        //// 淡入
        //t = 0f;
        //while (t < duration)
        //{
        //    managedAudio.Source.volume = Mathf.Lerp(0f, 1f, t / duration);
        //    t += Time.deltaTime;
        //    yield return null;
        //}
        //managedAudio.Source.volume = 1f;
    }
}
