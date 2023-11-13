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
    [SerializeField] private GameInput gameInput;
    public Transform orientation;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
 

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float sprintSpeed = 1f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpForce = 5f;
    public float acceleration = 5.0f; // Acceleration rate
    public float deceleration = 5.0f; // Deceleration 
    private float currentSpeed = 0.0f; // Current character speed

    [Header("Statistics")]
    public static float health = 50f;
    public static float score = 0f;
    //TEMPORARY (MAKE ENTIRE HANDLER FOR THESE SYSTEMS
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private Vector2 inputVector;
    private Vector3 moveDirection;
    private Vector3 yVelocity;
    public CharacterController characterController;
    private Animator playerAnimator;
    private PlayerAnimationHandler playerAnimationHandler;

    
    private float standingHeight;
    private bool isGrounded;
    private bool isWalking = false;
    private bool gameOver = false;
    

    [Header("Interacting")]
    [SerializeField] private float interactDistance;
    


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = characterController.height;
        

        
        playerAnimator = GetComponentInChildren<Animator>();
        playerAnimationHandler= GetComponent<PlayerAnimationHandler>();
        healthText.SetText("Health: " + health);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        gameInput.TickInput(delta);

        // Set Vector based on inputs
        inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = movementVector != Vector3.zero;

        // Handle Movement mechanics and Ground checks
        if (gameInput.Jumped() && isGrounded)
            Jump();
        Crouch();
        IsWalking();
        GroundChecking();
        HandleDash();
        DecreaseHealth();
        GameOver();

        scoreText.SetText("Score: " + score);
    }

    private void FixedUpdate()
    {
        // Update movement
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * inputVector.y + orientation.right * inputVector.x;

        // Determine if the player is sprinting
        bool isSprinting = isGrounded && gameInput.IsSprinting() && !gameInput.IsCrouching();

        // Calculate the horizontal speed based on sprinting state
        float targetSpeed = GetHorizontalSpeed(moveDirection, isSprinting);

        // Apply acceleration and deceleration
        if (targetSpeed > currentSpeed)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
        }

        // Update player movement with speed and direction
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        playerAnimator.SetFloat("IdleToRun", currentSpeed);

        // Update player with the force of gravity 
        yVelocity.y += gravity * Time.deltaTime;
        characterController.Move(yVelocity * Time.deltaTime);
    }

    public void HandleDash()
    {
        if (playerAnimator.GetBool("isInteracting"))
        { return; }


        if (!gameInput.dashFlag && gameInput.HandleDashInput())
        {
            Debug.Log("Animation should play");
            
            playerAnimationHandler.PlayTargetAnimation("DashBack", true);
        }
        
    }

    private void Jump()
    {
        yVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    private void Crouch()
    {
        if (gameInput.IsCrouching())
        {
            // Half height
            characterController.height = standingHeight / 2; 
        }
        else
        {
            // Reset height
            characterController.height = standingHeight;
        }
    }

    public RaycastHit InteractableCheck()
    {
        Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out RaycastHit hit, interactDistance);
        Debug.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, Color.red);
        return hit;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    

    // Ground Checking
    private void GroundChecking()
    {
        if (characterController.isGrounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (characterController.isGrounded && yVelocity.y < 0)
        {
            yVelocity.y = -2f;
        }
    }

  

    private float GetHorizontalSpeed(Vector3 moveDirection, bool isSprinting)
    {
        // Calculate the horizontal speed by ignoring the vertical component.
        Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);

        // Normalize the vector to ensure the speed is between 0 and 1.
        float horizontalSpeed = horizontalVelocity.normalized.magnitude;

        // If the player is sprinting, increase the horizontal speed.
        if (isSprinting)
        {
            horizontalSpeed *= sprintSpeed;
        }
        else
        {
            horizontalSpeed *= moveSpeed;
        }

        return horizontalSpeed;
    }
    private void DecreaseHealth()
    {
        if (gameInput.DecreaseHealth())
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
