using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class GameSound
{
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
    public bool disableInMenu;
    public bool endOnReload = true;
}
