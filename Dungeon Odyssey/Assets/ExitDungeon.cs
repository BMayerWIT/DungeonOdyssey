using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDungeon : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        if (collider.tag == "Player")
        {
            PlayerPrefs.SetInt("LoadIntro", 0);
            PlayerPrefs.SetInt("StatsWindowBool", 1);
            StatsHandler.Instance.SaveCurrentXPAndLevel();
            SaveAndLoad.SaveStats();
            SceneManager.LoadScene("Menu");

            
        }
    }

   
}
