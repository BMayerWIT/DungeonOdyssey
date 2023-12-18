using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider fpSensSlider;
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider SFXVolSlider;
    

    private void Update()
    {
       if (masterVolSlider != null)
        {
            masterVolSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume"));
        }
        fpSensSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("firstPersonSensitivity"));
        if (musicVolSlider != null)
        {
            musicVolSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVolume"));
        }
        if (SFXVolSlider != null)
        {
            SFXVolSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume"));
        }
        

    }
    public void SetFirstPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("firstPersonSensitivity", sensitivity);
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    
}
