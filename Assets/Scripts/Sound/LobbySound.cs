using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbySound : MonoBehaviour
{

    public AudioSource audioSourceBG;
    public AudioSource audioSourceSFX;
    public AudioClip[] sfx;

    //Config-----------------------------
    public Slider musicVolume, sfxVolume;
    public TextMeshProUGUI txtMusicVolume, txtSFXVolume;


    private void Start()
    {
        if(!PlayerPrefs.HasKey("music"))
            PlayerPrefs.SetFloat("music", .3f);

        if(!PlayerPrefs.HasKey("sfx"))
            PlayerPrefs.SetFloat("sfx", .3f);


        musicVolume.maxValue = 1;
        sfxVolume.maxValue = 1;

        musicVolume.value = PlayerPrefs.GetFloat("music");
        sfxVolume.value = PlayerPrefs.GetFloat("sfx");

        SetLobbySoundVolume();
        SetTextVolumeValue();
    }

    public void SetTextVolumeValue() {
        txtMusicVolume.text = "Music: " + (musicVolume.value * 100).ToString("F0") + "%";
        txtSFXVolume.text = "SFX: " + (sfxVolume.value * 100).ToString("F0") + "%";
    }

    public void SetLobbySoundVolume()
    {
        audioSourceBG.volume = PlayerPrefs.GetFloat("music");
        audioSourceSFX.volume = PlayerPrefs.GetFloat("sfx");
    }

    public void SaveVolumeValue()
    {
        PlayerPrefs.SetFloat("sfx", sfxVolume.value);
        PlayerPrefs.SetFloat("music", musicVolume.value);
        SetLobbySoundVolume();
    }


    public void PlayBtnHoverSFX() => audioSourceSFX.PlayOneShot(sfx[0]);
    public void PlayBtnPressedSFX() => audioSourceSFX.PlayOneShot(sfx[1]);
    public void PlayBtnPressedReset() => audioSourceSFX.PlayOneShot(sfx[2]);
    public void PlayBtnPressedConfirm() => audioSourceSFX.PlayOneShot(sfx[3]);
}
