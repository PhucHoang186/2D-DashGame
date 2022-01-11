using System;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.name = s.soundName;
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        PLayMusic("Background");
    }
    public void PLayMusic(string _name)
    {
        Sound sound = Array.Find(sounds, sound => sound.soundName == _name);
        sound.source.Play();
    }
    public bool ToggleMusic(string _name)
    {
        Sound sound = Array.Find(sounds, sound => sound.soundName == _name);
        sound.source.mute = !sound.source.mute;
        return sound.source.mute;
    }
}
