using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicValue", 0.75f);
        soundSlider.value = PlayerPrefs.GetFloat("SFXValue", 0.75f);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(soundSlider.value);
    }


    public void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("musicVol", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicValue", value);
    }
    public void SetSFXVolume(float value)
    {
        mainMixer.SetFloat("sfxVol", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXValue", value);
    }

}
