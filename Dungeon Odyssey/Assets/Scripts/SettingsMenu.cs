using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void SetFirstPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("firstPersonSensitivity", sensitivity);
    }

    public void SetThirdPersonSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("thirdPersonSensitivity", sensitivity);
    }
}
