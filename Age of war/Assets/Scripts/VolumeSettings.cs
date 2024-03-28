using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;


    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mainMixer.SetFloat("musique", Mathf.Log10(volume)*20);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        mainMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }
}
