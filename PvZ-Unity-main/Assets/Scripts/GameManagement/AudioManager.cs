// AudioManager.cs (�����������ֹ���)
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private const int MaxSimultaneousPerClip = 3;

    [SerializeField]
    private List<AudioClip> soundEffects = new List<AudioClip>();

    private Dictionary<int, List<AudioSource>> _soundEffectSources;

    // ��������������Դ�б�
    private List<AudioSource> _sceneMusicSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _soundEffectSources = new Dictionary<int, List<AudioSource>>();
        for (int i = 0; i < soundEffects.Count; i++)
        {
            CreateAudioSourcesForClip(i);
        }

        // �ӱ��ض�ȡ�ϴ�����
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", sfxVolume);

        ApplyVolumes();

        // ���ĳ����л��¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        
    }


    // ���ų������֣�������Դ����������Ч��
    public AudioSource PlaySceneMusic(AudioClip clip, bool loop)
    {
        if (clip == null)
        {
            Debug.LogWarning("����ĳ������ּ���Ϊ�ա�");
            return null;
        }

        var src = gameObject.AddComponent<AudioSource>();
        src.clip = clip;
        src.loop = loop;
        src.volume = musicVolume;
        src.playOnAwake = false;
        src.Play();

        _sceneMusicSources.Add(src);
        return src;
    }

    public void StopSceneMusic(AudioClip clip)
    {
        for (int i = _sceneMusicSources.Count - 1; i >= 0; i--)
        {
            var src = _sceneMusicSources[i];
            if (src.clip == clip)
            {
                src.loop = false;
                src.Stop();
                Destroy(src);
                _sceneMusicSources.RemoveAt(i);
            }
        }
    }

    public void StopAllSceneMusic()
    {
        foreach (var src in _sceneMusicSources)
        {
            src.loop = false;
            src.Stop();
            Destroy(src);
        }
        _sceneMusicSources.Clear();
    }

    // ����Ϊԭ�з�����δ���䶯
    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Count)
        {
            AudioSource source = GetAvailableSource(index);
            if (source != null)
            {
                source.clip = soundEffects[index];
                source.loop = false;
                source.Play();
            }
            else
            {
                //Debug.LogWarning($"������Դ���ڲ��ţ�{soundEffects[index].name}");
            }
        }
        else
        {
            Debug.LogWarning("Sound effect index out of range: " + index);
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        PlaySoundEffect(clip, false);
    }

    public AudioSource PlaySoundEffect(AudioClip clip, bool loop)
    {
        if (clip == null)
        {
            Debug.LogWarning("�������Ƶ����Ϊ�ա�");
            return null;
        }

        int index = soundEffects.IndexOf(clip);
        if (index == -1)
        {
            soundEffects.Add(clip);
            index = soundEffects.Count - 1;
            CreateAudioSourcesForClip(index);
        }

        AudioSource source = GetAvailableSource(index);
        if (source != null)
        {
            source.clip = clip;
            source.loop = loop;
            source.volume = sfxVolume;
            source.Play();
            return source;
        }
        else
        {
            Debug.LogWarning($"������Դ���ڲ��ţ�{clip.name}");
            return null;
        }
    }

    public void StopLooping(AudioClip clip)
    {
        if (clip == null)
            return;

        int index = soundEffects.IndexOf(clip);
        if (index == -1)
        {
            Debug.LogWarning($"δ�������Ƶ������{clip.name}");
            return;
        }

        foreach (var source in _soundEffectSources[index])
        {
            if (source.isPlaying && source.loop && source.clip == clip)
            {
                source.loop = false;
                source.Stop();
            }
        }
    }

    public void PlaySoundEffectByName(string clipName)
    {
        for (int i = 0; i < soundEffects.Count; i++)
        {
            if (soundEffects[i] != null && soundEffects[i].name == clipName)
            {
                PlaySoundEffect(i);
                return;
            }
        }
        Debug.LogError($"δ�ҵ���Ϊ \"{clipName}\" ����Ч������ soundEffects �б��ʹ�� PlaySoundEffect(AudioClip, bool) ��̬��ӡ�");
    }

    private AudioSource GetAvailableSource(int clipIndex)
    {
        foreach (var source in _soundEffectSources[clipIndex])
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    private void CreateAudioSourcesForClip(int index)
    {
        var sourceList = new List<AudioSource>();
        for (int j = 0; j < MaxSimultaneousPerClip; j++)
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.loop = false;
            newSource.playOnAwake = false;
            sourceList.Add(newSource);
        }
        _soundEffectSources[index] = sourceList;
    }

    public void StopAllSoundEffects()
    {
        foreach (var sourceList in _soundEffectSources.Values)
        {
            foreach (var source in sourceList)
            {
                source.loop = false;
                source.Stop();
            }
        }
    }

    [Range(0f, 1f)]
    public float musicVolume = 0.6f;     // ������������
    [Range(0f, 1f)]
    public float sfxVolume = 0.6f;       // ��Ч����


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �г���ʱ��Ȼ��������
        ApplyVolumes();
        StopAllSceneMusic();
    }

    /// <summary>Ӧ�õ�ǰ������������Դ</summary>
    private void ApplyVolumes()
    {
        // �Ƴ��ѱ����ٵ�����
        _sceneMusicSources.RemoveAll(src => src == null);

        // ������������
        foreach (var src in _sceneMusicSources)
        {
            if (src != null)
                src.volume = musicVolume;
        }

        // ��Ч����
        foreach (var list in _soundEffectSources.Values)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var src = list[i];
                if (src != null)
                {
                    src.volume = sfxVolume;
                }
                else
                {
                    list.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>�ⲿ������������</summary>
    public void SetMusicVolume(float vol)
    {
        musicVolume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        ApplyVolumes();
    }

    /// <summary>�ⲿ������Ч����</summary>
    public void SetSFXVolume(float vol)
    {
        sfxVolume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        ApplyVolumes();
    }
}