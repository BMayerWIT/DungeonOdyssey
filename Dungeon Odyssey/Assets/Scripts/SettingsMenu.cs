using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider fpSensSlider;
    

    private void Update()
    {
       
        fpSensSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("firstPersonSensitivity"));
        
    }
    public void SetFirstPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("firstPersonSensitivity", sensitivity);
    }

    
}
