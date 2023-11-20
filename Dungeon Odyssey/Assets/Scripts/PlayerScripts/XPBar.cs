using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetXPToLevelUp(int xpToLevelUp)
    {
        slider.maxValue = xpToLevelUp;
    }

    public void SetCurrentXP(int currentXP)
    {
        slider.value = currentXP;
    }
}
