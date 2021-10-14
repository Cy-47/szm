using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource _audioSource;
    public AudioClip hitSoundFx;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.6f;
    }

    public void SetMusic(AudioClip music)
    {
        //print(music.ToString());
        _audioSource.clip = music;
    }

    public void SetPitch(float pitch)
    {
        _audioSource.pitch = pitch;
    }
    
    public void PlayHitEffect()
    {
        _audioSource.PlayOneShot(hitSoundFx, 3f);
    }

    public void Pause()
    {
        _audioSource.Pause();
    }
    public void PlayMusicFrom(float startTime)
    {
        _audioSource.time = startTime;
        _audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
