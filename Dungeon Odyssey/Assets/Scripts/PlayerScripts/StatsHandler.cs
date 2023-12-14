using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StatsHandler : MonoBehaviour
{
    public static StatsHandler Instance;

    [Header("Health")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    [Header("Stamina")]

    [Header("XP")]
    public int currentPlayerLevel = 1;
    public int xpToLevelUp = 0;
    public int currentXP;

    

    public HealthBar healthBar;
    public XPBar xpBar;
    public TextMeshProUGUI currentLevelText;

    private void Start()
    {
        // Set Up Healthbar
        SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

       
        currentLevelText.SetText("" + currentPlayerLevel);

    }

    private void Update()
    {
        LevelUp();
        if (Input.GetKeyDown(KeyCode.L))
        {
            
            AddXP(10);
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
            currentXP = 0;
            SetXPToLevelUp();
            xpBar.SetCurrentXP(currentXP);

        }
    }

    public void AddXP(int xpAmount)
    {
        currentXP += xpAmount;
        xpBar.SetCurrentXP(currentXP);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetCurrentHealth(currentHealth);
    }

    private void GameOver()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
