using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSound : MonoBehaviour
{
    public AudioSource musicAudioSources;
    public AudioSource[] sfxAudioSources;



    private void Start()
    {

        foreach (var item in sfxAudioSources)
        {
            item.volume = PlayerPrefs.GetFloat("sfx");
        }
        musicAudioSources.volume = PlayerPrefs.GetFloat("music");
    }
}
