using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Animator ani;
    private void Start()
    {
        source.Play();
        ani.SetBool("Active", !source.mute);
    }
    public void Toggle()
    {
        SoundManager.Instance.ToggleMusic(source);
        ani.SetBool("Active", !source.mute);

    }
}
