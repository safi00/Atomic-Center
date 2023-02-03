using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource effectsSource;
    [SerializeField] public static float volume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void playSound(AudioClip audioClip)
    {
        effectsSource.PlayOneShot(audioClip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        volume = AudioListener.volume * 100;
    }
    public void VolumeUp()
    {
        float volumeTemp = AudioListener.volume * 100;
        switch (volumeTemp)
        {
            case 1f:
                AudioListener.volume = 0.05f;
                volume = AudioListener.volume * 100;
                break;
            case < 100f:
                AudioListener.volume += 0.05f;
                volume = AudioListener.volume * 100;
                break;
        }
    }
    public void VolumeDown()
    {
        float volumeTemp = AudioListener.volume * 100;
        switch (volumeTemp)
        {
            case >= 7f:
                AudioListener.volume -= 0.05f;
                volume = AudioListener.volume * 100;
                break;
            case <= 6f:
                AudioListener.volume = 0.01f;
                volume = AudioListener.volume * 100;
                break;
        }
    }
    //mute button
    public void ToggleMusic()
    {
        musicSource.mute= !musicSource.mute;
    }
}
