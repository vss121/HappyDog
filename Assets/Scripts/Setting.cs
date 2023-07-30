using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider audioSlider;
    public string SoundGroup;

    public float sound;
    public float SaveVolume;
    private void Start()
    {
        if (PlayerPrefs.HasKey(SoundGroup))
        {
            SaveVolume = PlayerPrefs.GetFloat(SoundGroup);
            audioSlider.value = SaveVolume;
        }
        else
        {
            SaveVolume = audioSlider.value;
            PlayerPrefs.SetFloat(SoundGroup, SaveVolume);
        }
        PlayerPrefs.Save();
    }
    public void AudioControl()
    {
        sound = audioSlider.value;
        if(sound == -40f)
        {
            mixer.SetFloat(SoundGroup, -80);
        }
        else
        {
            mixer.SetFloat(SoundGroup, sound);
        }
        PlayerPrefs.SetFloat(SoundGroup, sound);
        PlayerPrefs.Save();
    }
    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}