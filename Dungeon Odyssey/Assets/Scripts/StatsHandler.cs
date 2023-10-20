using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    

    public static void SaveData()
    {
        PlayerPrefs.SetFloat("Health", Player.health);
        PlayerPrefs.SetFloat("Score", Player.score);
    }

    public static void LoadData()
    {
        Player.health = PlayerPrefs.GetFloat("Health");
        Player.score = PlayerPrefs.GetFloat("Score");
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
