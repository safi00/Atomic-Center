using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtons : MonoBehaviour
{
    [Header("Volume essentials")]
    [SerializeField] public Text volume;
    [SerializeField] private bool toggleMusic;
    [SerializeField] private bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        toggleMusic = false;
        isMuted = false;
        SoundManager.Instance.ChangeMasterVolume(0.05f);
    }

    public void VolumeUp()
    {
        SoundManager.Instance.VolumeUp();
        if (!isMuted)
        {
            volume.text = "Volume: " + SoundManager.volume + "%";
        }
    }
    public void VolumeDown()
    {
        SoundManager.Instance.VolumeDown();
        if (!isMuted)
        {
            volume.text = "Volume: " + SoundManager.volume + "%";
        }
    }
    //Mute button
    public void toggle()
    {
        if (!toggleMusic)
        {
            SoundManager.Instance.ToggleMusic();
            isMuted= !isMuted;
            if (isMuted)
            {
                volume.text = "Volume: MUTED";
            }
            else
            {
                volume.text = "Volume: " + SoundManager.volume + "%";
            }
        }
    }
}