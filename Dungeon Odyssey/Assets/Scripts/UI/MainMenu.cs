using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("LoadIntro") == 1)
        {
            GameObject.Find("LoadingScreenOnStart").SetActive(true);
        }
        else
        {
            GameObject.Find("LoadingScreenOnStart").SetActive(false);
            AudioManager.Instance.PlayMainMenuMusic();
        }

        if (PlayerPrefs.GetInt("StatsWindowBool") == 1)
        {
            GameObject.Find("StatsWindow").SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            GameObject.Find("StatsWindow").SetActive(false);
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("LoadIntro", 0);
        SceneManager.LoadScene("ProcGen");
       
        Time.timeScale = 1f;

    }

    public void QuitGame() 
    {
        PlayerPrefs.SetInt("LoadIntro", 1);
        Debug.Log("Quit!");
        Application.Quit();
    }
}
    