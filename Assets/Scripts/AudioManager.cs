using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameSound[] sounds;
    public static AudioManager instance;
    public bool keep;

    void Awake()
    {
        if (keep)
        {
            if (instance == null)
                instance = this;
            else
            {
                foreach (GameSound s in instance.sounds)
                    if (s.endOnReload)
                        s.source.Stop();
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        foreach (GameSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        UpdateAllSounds();
    }

    void Update()
    {
        UpdateAllSounds();
    }

    public void Play(string name)
    {
        GameSound s = Array.Find(sounds, sound => sound.clip.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        GameSound s = Array.Find(sounds, sound => sound.clip.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateAllSounds()
    {
        SaveSystem.LoadData();
        foreach (GameSound s in sounds)
        {
            UpdateSound(s);
        }
    }

    public void UpdateSound(GameSound s)
    {
        s.source.pitch = s.pitch;
        s.source.volume = s.volume * SaveSystem.data.volume;
    }

    public void SetVolume(float volume)
    {
        SaveSystem.data.volume = volume;
        SaveSystem.SaveData();
    }

    public void PlaySample()
    {
        Play("jump_pad_launch");
    }
}