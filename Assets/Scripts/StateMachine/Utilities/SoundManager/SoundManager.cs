using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] MusicSounds, SfxSounds;
    public AudioSource MusicSource, SfxSource,PS,ES;
    private void Awake()
    {
    if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        repeatTheme();
    }

    public void repeatTheme()
    {
        InvokeRepeating("PlayTheme", 0f, 124f);
    }
    private void PlayTheme()
    {
        SoundManager.instance.PlayMusic("BGM");
    }

    public void PlayWalk()
    {
        PlaySfx("PlayerWalk");
    }

    private void Update()
    {

    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("无法找到音乐");
        }
        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(SfxSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("无法找到音效");
        }
        else
        {
            SfxSource.clip = s.Clip;
            SfxSource.Play();
        }
    }

    public void PlayPSfx(string name)
    {
        Sound s = Array.Find(SfxSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("无法找到音效");
        }
        else
        {
            PS.clip = s.Clip;
            PS.Play();
        }
    }

    public void PlayESfx(string name)
    {
        Sound s = Array.Find(SfxSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("无法找到音效");
        }
        else
        {
            ES.clip = s.Clip;
            ES.Play();
        }
    }

    //暂停和调整声音的方法
    public void StopMusic()
    {
        MusicSource.Stop();
    }
    public void StopSFX()
    {
        SfxSource.Stop();

        //CancelInvoke();
    }
    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
    }
    public void ToggleSfx()
    {
        SfxSource.mute = !SfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        SfxSource.volume = volume;
    }
}

