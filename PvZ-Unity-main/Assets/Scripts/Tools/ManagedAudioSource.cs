// ManagedAudioSource.cs (新增场景音乐选项)
using UnityEngine;

public class ManagedAudioSource : MonoBehaviour
{
    [Header("要播放的音效")]
    public AudioClip clip;

    [Header("播放设置")]
    public bool playOnAwake = false;
    public bool loop = false;

    [Header("场景音乐")]  // 新增勾选项
    public bool isSceneMusic = false;

    private void Start()
    {
        if (playOnAwake && clip != null)
        {
            Play();
        }
    }

    /// <summary>
    /// 通过 AudioManager 播放此组件设置的音效
    /// </summary>
    public void Play()
    {
        if (clip == null)
        {
            Debug.LogWarning($"[{name}] 未指定要播放的 AudioClip。即将跳过播放。");
            return;
        }

        if (isSceneMusic)
        {
            AudioManager.Instance.PlaySceneMusic(clip, loop);
        }
        else
        {
            AudioManager.Instance.PlaySoundEffect(clip, loop);
        }
    }

    /// <summary>
    /// 停止当前正在播放的循环（如果有）
    /// </summary>
    public void Stop()
    {
        if (clip == null) return;

        if (isSceneMusic)
            AudioManager.Instance.StopSceneMusic(clip);
        else
            AudioManager.Instance.StopLooping(clip);
    }
}