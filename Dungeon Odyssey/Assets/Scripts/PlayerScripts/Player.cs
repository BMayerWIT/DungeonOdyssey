using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    [Header("Objects")]
    
    [SerializeField] private Camera firstPersonCamera;
    
 

    
    [Header("Statistics")]
    public static float health = 50f;
    public static float score = 0f;
    //TEMPORARY (MAKE ENTIRE HANDLER FOR THESE SYSTEMS
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;


    
    private Animator playerAnimator;
    private PlayerAnimationHandler playerAnimationHandler;

    
    private float standingHeight;
    private bool isPlayerGrounded;
    
    private bool gameOver = false;
    

    [Header("Interacting")]
    [SerializeField] private float interactDistance;
    


    private void Start()
    {
        
       // standingHeight = characterController.height;
        

        
        playerAnimator = GetComponentInChildren<Animator>();
        playerAnimationHandler= GetComponent<PlayerAnimationHandler>();
        healthText.SetText("Health: " + health);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        GameInput.inputInstance.TickInput(delta);

        


       
        
        HandleDash();
        DecreaseHealth();
        GameOver();

        scoreText.SetText("Score: " + score);
        
    }

    

   

    public void HandleDash()
    {
        //if (playerAnimator.GetBool("isInteracting"))
        //{ return; }


        if (!GameInput.inputInstance.dashFlag && GameInput.inputInstance.HandleDashInput())
        {
            Debug.Log("Animation should play");
            
            //playerAnimationHandler.PlayTargetAnimation("DashBack", true);
        }
        
    }

  

    public RaycastHit InteractableCheck()
    {
        Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out RaycastHit hit, interactDistance);
        Debug.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, Color.red);
        return hit;
    }

    

   
    private void DecreaseHealth()
    {
        if (GameInput.inputInstance.DecreaseHealth())
        { 
            health -= 5;
            healthText.SetText("Health: " + health);
        }
        if (health <= 0)
        {
            gameOver = true;
        }
    }

    private void GameOver()
    {
        if (gameOver)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
}
