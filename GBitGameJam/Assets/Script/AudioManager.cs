using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]public static AudioManager Instance => instance;
    private static AudioManager instance;

    public AudioMixer audioMixer;

    public int audioSourceNum = 5;
    public List<SoundAudioClip> soundAudioClips = new List<SoundAudioClip>();
    
    [SerializeField] private List<AudioSource> _audioSourceList = new List<AudioSource>();

    public float audioVolume;
    
    
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        AudioSourceCheck();

        audioMixer.SetFloat("AudioVolume", audioVolume);
    }
    
    private void AudioSourceCheck()
    {
        foreach (var audio in _audioSourceList)
        {
            if (audio == null)
            {
                _audioSourceList.Remove(audio);
            }
        }

        int n = audioSourceNum - _audioSourceList.Count;
        
        for (int i = 0; i < n; i++)
        {
            GenerateAudioSource();
        }
    }
    
    private AudioSource GenerateAudioSource()
    {
        GameObject obj = new GameObject("Sound");
        obj.AddComponent<AudioSource>();
        _audioSourceList.Add(obj.GetComponent<AudioSource>());

        return obj.GetComponent<AudioSource>();
    }

    private void ClearAllAudioSource()
    {
        _audioSourceList.Clear();
    }

    private AudioClip SearchAudioClip(Sound sound)
    {
        foreach (var clip in soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.audioClip;
            }
        }

        return null;
    }

    private AudioMixerGroup SearchAudioGroup(Sound sound)
    {
        foreach (var clip in soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.group;
            }
        }

        return null;
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (var source in _audioSourceList)
        {
            if (IsAudioSourcePlay(source))
            {
                return source;
            }
        }
        
        return GenerateAudioSource();
    }

    private bool IsAudioSourcePlay(AudioSource audioSource)
    {
        if (audioSource.isPlaying == false)
        {
            return true;
        }
        return false;
    }
    
    public void SetMasterVolume(Scrollbar bar)
    {
        float volume = Mathf.Lerp(0, -40,  1 - bar.value);
        audioMixer.SetFloat("MasterAudioVolume", volume);
    }

    public void SetMusicVolume(Scrollbar bar)
    {
        float volume = Mathf.Lerp(0, -40, 1 - bar.value);
        audioMixer.SetFloat("MusicVolume", volume);
    }
    
    public void SetEffectVolume(Scrollbar bar)
    {
        float volume = Mathf.Lerp(0, -40, 1 - bar.value);
        audioMixer.SetFloat("SoundEffectVolume", volume);
    }

    /// <summary>
    /// Play one shot
    /// </summary>
    /// <param name="sound"></param>
    public AudioSource PlayAudio(Sound sound)
    {
        AudioSource source = GetFreeAudioSource();
        if (source)
        {
            source.outputAudioMixerGroup = SearchAudioGroup(sound);
            source.clip = SearchAudioClip(sound);
            source.transform.position = Camera.main.transform.position;
            source.Play();
        }

        return source;
    }

    /// <summary>
    /// 3D声效，根据输入位置声音移动到对应的坐标
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public void PlayAudio3D(Sound sound ,Vector3 position)
    {

        AudioSource source = GetFreeAudioSource();
        
        source.transform.position = position;
        if (source)
        {
            source.outputAudioMixerGroup = SearchAudioGroup(sound);
            source.clip = SearchAudioClip(sound);
            source.Play();
        }
    }

    public void PlayBGM(Sound sound)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = SearchAudioClip(sound);
        source.Play();
    }

}

public enum Sound
{
    SecondPoint,
    MinPoint,
    HourPoint,
    AlarmRing,
    Jump,
    PrepareFast,
    PrepareNormal,
    PrepareSlow,
    ReachFail,
    ReachSuccess,
    BubbleExplore,
    Sleeping,
    None
}

[System.Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
    public AudioMixerGroup group;
}