// AudioManager.cs (新增场景音乐管理)
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

    // 新增：场景音乐源列表
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

        // 从本地读取上次设置
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", sfxVolume);

        ApplyVolumes();

        // 订阅场景切换事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        
    }


    // 播放场景音乐：独立来源，不加入音效池
    public AudioSource PlaySceneMusic(AudioClip clip, bool loop)
    {
        if (clip == null)
        {
            Debug.LogWarning("传入的场景音乐剪辑为空。");
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

    // 以下为原有方法，未作变动
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
                //Debug.LogWarning($"所有音源正在播放：{soundEffects[index].name}");
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
            Debug.LogWarning("传入的音频剪辑为空。");
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
            Debug.LogWarning($"所有音源正在播放：{clip.name}");
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
            Debug.LogWarning($"未管理的音频剪辑：{clip.name}");
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
        Debug.LogError($"未找到名为 \"{clipName}\" 的音效，请检查 soundEffects 列表或使用 PlaySoundEffect(AudioClip, bool) 动态添加。");
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
    public float musicVolume = 0.6f;     // 场景音乐音量
    [Range(0f, 1f)]
    public float sfxVolume = 0.6f;       // 音效音量


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 切场景时依然保持音量
        ApplyVolumes();
        StopAllSceneMusic();
    }

    /// <summary>应用当前音量到所有音源</summary>
    private void ApplyVolumes()
    {
        // 移除已被销毁的引用
        _sceneMusicSources.RemoveAll(src => src == null);

        // 场景音乐音量
        foreach (var src in _sceneMusicSources)
        {
            if (src != null)
                src.volume = musicVolume;
        }

        // 音效音量
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

    /// <summary>外部设置音乐音量</summary>
    public void SetMusicVolume(float vol)
    {
        musicVolume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        ApplyVolumes();
    }

    /// <summary>外部设置音效音量</summary>
    public void SetSFXVolume(float vol)
    {
        sfxVolume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        ApplyVolumes();
    }
}