using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
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
        if (GameInput.inputInstance.ReturnPauseState())
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
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
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HUD.SetActive(true);
    }

    void Pause()
    {
        menuList[0].SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        HUD.SetActive(false);
        
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
        SceneManager.LoadScene("Menu");

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
