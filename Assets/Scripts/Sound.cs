using UnityEngine;
[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string soundName;
    [Range(0, 1)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;

}
