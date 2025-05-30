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

        // ����ڱ༭��������Ϊ��Awakeʱ���ţ��Զ�ִ�в���
        if (managedAudio.playOnAwake && managedAudio.clip != null)
        {
            managedAudio.Play();
        }
    }

    /// <summary>
    /// ƽ���л����֣���������������������
    /// </summary>
    public void changeMusicSmoothly(string name)
    {
        musicName = name;
        StartCoroutine(FadeOutIn());
    }

    /// <summary>
    /// �����л����֣��������뵭��
    /// </summary>
    public void changeMusic(string name = null)
    {
        if (!string.IsNullOrEmpty(name))
            musicName = name;

        AudioClip clip = Resources.Load<AudioClip>("Sounds/Background/" + musicName);
        if (clip == null)
        {
            Debug.LogWarning($"δ�ҵ������ļ�: {musicName}");
            return;
        }

        AudioManager.Instance.StopAllSceneMusic();

        managedAudio.clip = clip;
        managedAudio.loop = true;
        managedAudio.Play();
    }

    /// <summary>
    /// ���� Animator
    /// </summary>
    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    private IEnumerator FadeOutIn()
    {
        //float duration = 0.5f;
        //float startVolume = managedAudio.Source.volume;

        //// ����
        //float t = 0f;
        //while (t < duration)
        //{
        //    managedAudio.Source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
        //    t += Time.deltaTime;
        //    yield return null;
        //}
        //managedAudio.Source.volume = 0f;
        yield return null;
        // �л�����
        changeMusic();

        //// ����
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
