using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public void SetEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
    }
    public void SetNormal()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
    }
    public void SetHard()
    {
        PlayerPrefs.SetInt("Difficulty", 3);
    }
    
}
