// ManagedAudioSource.cs (������������ѡ��)
using UnityEngine;

public class ManagedAudioSource : MonoBehaviour
{
    [Header("Ҫ���ŵ���Ч")]
    public AudioClip clip;

    [Header("��������")]
    public bool playOnAwake = false;
    public bool loop = false;

    [Header("��������")]  // ������ѡ��
    public bool isSceneMusic = false;

    private void Start()
    {
        if (playOnAwake && clip != null)
        {
            Play();
        }
    }

    /// <summary>
    /// ͨ�� AudioManager ���Ŵ�������õ���Ч
    /// </summary>
    public void Play()
    {
        if (clip == null)
        {
            Debug.LogWarning($"[{name}] δָ��Ҫ���ŵ� AudioClip�������������š�");
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
    /// ֹͣ��ǰ���ڲ��ŵ�ѭ��������У�
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