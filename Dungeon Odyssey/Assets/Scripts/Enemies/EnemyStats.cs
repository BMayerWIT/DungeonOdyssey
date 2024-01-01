using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel;
    public int maxHealth;
    public int currentHealth;
    public Material bodyMat;
    public float dissolveSpeed = 0.1f;
    float dissolveAmount = -1f;
    public bool gotHit;
    public int xpWorth;
    public int goldWorth;
    private Animator animator;


    // MATERIAL INSTANCING FOR MINOTAUR ENEMY.
    //public Material baseMaterial;

    //void Start()
    //{
    //    // Check if the base material is assigned
    //    if (baseMaterial != null)
    //    {
    //        // Create a material instance
    //        Material materialInstance = Instantiate(baseMaterial);

    //        // Apply the material instance to the renderer (assuming renderer is on the same GameObject)
    //        Renderer renderer = GetComponent<Renderer>();
    //        if (renderer != null)
    //        {
    //            renderer.material = materialInstance;
    //        }
    //        else
    //        {
    //            Debug.LogError("Renderer component not found on the GameObject.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Base Material is not assigned.");
    //    }
    //}

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        healthLevel = 25 * PlayerPrefs.GetInt("Difficulty");
        if (StatsHandler.Instance.currentPlayerLevel < 5)
        {
            healthLevel += 25;
        }
        else if (StatsHandler.Instance.currentPlayerLevel > 5 && StatsHandler.Instance.currentPlayerLevel < 7)
        {
            healthLevel += 50;
        }
        else if (StatsHandler.Instance.currentPlayerLevel >= 7)
        {
            healthLevel += 75;
        }
        
        SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            print("dissolving");
            StartCoroutine(Dissolve());
        }
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= damage;

        //animator.Play("Damage_01");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StatsHandler.Instance.XPAdder(xpWorth);
            StatsHandler.Instance.enemiesKilled += 1;
            Destroy(gameObject);
        }
        
    }

    private IEnumerator Dissolve()
    {
        for (int i = 0; i < 160; i++)
        {
            yield return new WaitForSeconds(dissolveSpeed);
            dissolveAmount += 0.01f;
            if (bodyMat != null)
            {
                // Set the float parameter in the Shader Graph
                bodyMat.SetFloat("_dissolveProgress", dissolveAmount);
            }
            else
            {
                Debug.Log("Material is not assigned.");
            }
        }
        dissolveAmount = -1f;

        
    }
}
