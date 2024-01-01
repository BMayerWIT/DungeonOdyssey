using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject HUD;

    [Header("Menus which must be hidden after game is unpaused")]

    [SerializeField] private GameObject[] menuList;
    
    private void Awake()
    {
        for (int i = 0; i < menuList.Length; i ++)
        {
            menuList[i].SetActive(false);
        }
    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.P))
        if (GameInput.inputInstance.ReturnPauseState())
        {
            print("pressed pause");
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    
    }

    public void Resume()
    {
        for (int i = 0; i < menuList.Length; i++)
        {
            menuList[i].SetActive(false);
        }
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HUD.SetActive(true);
        AudioManager.Instance.PlayUnlockSkillSound();


    }

    void Pause()
    {
        menuList[0].SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        HUD.SetActive(false);
        AudioManager.Instance.PlayUnlockSkillSound();
        
        
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        
        Time.timeScale = 1f;
        Resume();
    }

    public void ReturnToMenu()
    {
        Resume();
        
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerPrefs.SetInt("LoadIntro", 0);
        SceneManager.LoadScene("Menu");
        

    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("LoadIntro", 1);
        Application.Quit();
    }

}
