using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.playSound(clip);
    }
}
