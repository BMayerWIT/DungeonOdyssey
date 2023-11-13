using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider fpSensSlider;
    [SerializeField] private Slider tpSensSlider;

    private void Update()
    {
        fpSensSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("firstPersonSensitivity"));
        tpSensSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("thirdPersonSensitivity"));
    }
    public void SetFirstPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("firstPersonSensitivity", sensitivity);
    }

    public void SetThirdPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("thirdPersonSensitivity", sensitivity);
    }
}
