using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public void StartMenuMusic()
    {
        AudioManager.Instance.PlayMainMenuMusic();
    }
}
