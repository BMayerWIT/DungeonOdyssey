using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    string currentSceneName;

    private AudioSource masterAudioSource;
    private AudioSource musicAudioSource;
    private GameObject camera;

    public AudioClip buttonClick;
    public AudioClip equipSkill;
    public AudioClip unlockSkill;
    public AudioClip mainMenuMusic;

    public bool equipFlag;

    public float masterVolume = 1.0f;  // Default volume for button click
    public float SFXVolume = 1.0f;    // Default volume for equip skill
    public float musicVolume = 0.5f; // Default volume for main menu music


    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        masterAudioSource = camera.GetComponent<AudioSource>();
        musicAudioSource = GetComponent<AudioSource>();
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Equals("Menu"))
        {
        
        }
       
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        HandleVolumes();
    }

    private void HandleVolumes()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume") * masterVolume;
        musicVolume = PlayerPrefs.GetFloat("musicVolume") * masterVolume;
        musicAudioSource.volume = musicVolume;
    }

    public void PlayButtonSound()
    {
        masterAudioSource.PlayOneShot(buttonClick, SFXVolume);
    }

    public void PlayEquipSkillSound()
    {
        if (equipFlag)
        {
            masterAudioSource.PlayOneShot(equipSkill, SFXVolume);
            equipFlag = false;
        }
        
    }

    public void PlayUnlockSkillSound()
    {
        masterAudioSource.PlayOneShot(unlockSkill, SFXVolume);
    }

    public void PlayMainMenuMusic()
    {
        musicAudioSource.clip = mainMenuMusic;
        musicAudioSource.volume = musicVolume;
        musicAudioSource.Play();
    }
}
