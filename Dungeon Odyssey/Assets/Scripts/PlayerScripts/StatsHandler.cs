using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class StatsHandler : MonoBehaviour
{
    public static StatsHandler Instance;

    [Header("Health")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    [Header("Stamina")]
    public float maxStamina = 20f;
    public float staminaRechargeDelay = 10f; // Delay before stamina starts recharging
    public float staminaRechargeRate = 2f;  // Rate at which stamina recharges
    public float staminaDrainRate = 2f;
    public float stamina;
    public float timeSinceStaminaDrained;

    [Header("XP")]
    public int currentPlayerLevel = 1;
    public int xpToLevelUp = 0;
    public int currentXP;
    public float xpFillRate = 0.1f;
    public int skillPoints;
    private string currentSceneName;
    public LevelStatsData levelStatsData;
    public HealthBar healthBar;
    public XPBar xpBar;
    public StaminaBar staminaBar;
    public TextMeshProUGUI currentLevelText;

    public int enemiesKilled, xpGained, levelsAquired, totalDamage, damageTaken;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {

        levelStatsData = SaveAndLoad.LoadStats();
        currentPlayerLevel = levelStatsData.currentLevel;
        currentXP = levelStatsData.currentXP;
        skillPoints = levelStatsData.currentSkillPoints;
        currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName != "Menu")
        {
            // Set Up Healthbar
            SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            stamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);

            currentLevelText.SetText("" + currentPlayerLevel);

            SetXPToLevelUp();
            xpBar.SetCurrentXP(currentXP);
        }
        
    }

    private void Update()
    {
        if (currentSceneName != "Menu")
        {
            LevelUp();
            if (Input.GetKeyDown(KeyCode.L))
            {

                StartCoroutine(AddXP(10));
            }
            staminaBar.SetCurrentStamina(stamina);
            timeSinceStaminaDrained += Time.deltaTime;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetCurrentHealth(currentHealth);
        }
       
        
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private void SetXPToLevelUp()
    {
        xpToLevelUp = currentPlayerLevel * 10;
        xpBar.SetXPToLevelUp(xpToLevelUp);
    }

    private void LevelUp()
    {
        
        if (currentXP >= xpToLevelUp)
        {
            currentLevelText.SetText("" + currentPlayerLevel);
            currentPlayerLevel += 1;
            levelsAquired += 1;
            skillPoints++;
            currentXP = 0;
            SetXPToLevelUp();
            xpBar.SetCurrentXP(currentXP);

        }
    }

    public IEnumerator AddXP(int xpAmount)
    {
        float xpToAdd = 0;
        print("Working");
        while (xpToAdd <= xpAmount)
        {
            
            currentXP++;
            xpToAdd++;
            xpBar.SetCurrentXP(currentXP);
            yield return new WaitForSeconds(xpFillRate);
        }
        
        
        
    }

    public void XPAdder(int xp)
    {
        StartCoroutine(AddXP(xp));
        xpGained += xp;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageTaken += damage;
        healthBar.SetCurrentHealth(currentHealth);
    }


    public void HandleSprintingDrain()
    {


        if (stamina > 0f)
        {

            stamina -= Time.deltaTime * staminaDrainRate;
            timeSinceStaminaDrained = 0f;
        }
        
    }

    public void HandleSprintingRecharge()
    {
        
        staminaBar.SetCurrentStamina(stamina);
        if (stamina < maxStamina && timeSinceStaminaDrained > staminaRechargeDelay)
        {
            stamina += staminaRechargeRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
    }

    public float GetCurrentStamina()
    {
        return stamina;
    }

    private void GameOver()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void SaveCurrentXPAndLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", currentPlayerLevel);
        PlayerPrefs.SetInt("PlayerXP", currentXP);
        
    }

    public void ResetCurrentXPAndLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", 0);
        PlayerPrefs.SetInt("PlayerXP", 0);
        PlayerPrefs.SetInt("SkillPoints", 0);
    }
}
