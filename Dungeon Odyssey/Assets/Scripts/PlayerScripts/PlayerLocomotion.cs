using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [HideInInspector]
    public const float DEFAULT_MOVE_SPEED = 2f;
    [HideInInspector]
    public const float DEFAULT_SPRINT_SPEED = 5f;
    [HideInInspector]
    public const float DEFAULT_JUMP_FORCE = 0.7f;
    [HideInInspector]
    public const float DEFAULT_WALK_MULTIPLIER = 0;
    [HideInInspector]
    public const float DEFAULT_SPRINT_MULTIPLIER = 0;





    [Header("Movement Stats")]
    public float moveSpeed = 2f;
    public float sprintSpeed = 5f;
    [SerializeField] private float gravity = -9.8f;
    public float jumpForce = 0.7f;

    public float acceleration = 5.0f; // Acceleration rate
    public float deceleration = 5.0f; // Deceleration 
    public float currentSpeed = 0.0f; // Current character speed

    public Transform orientation;
    private Vector2 inputVector;
    private Vector3 moveDirection;

    private Vector3 yVelocity;
    private bool isWalking = false;
    public bool isPlayerGrounded;
    public bool isSprinting;
    [SerializeField] private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Set Vector based on inputs
        inputVector = GameInput.inputInstance.GetMovementVectorNormalized();
        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = movementVector != Vector3.zero;

        // Handle Movement mechanics and Ground checks
        GroundChecking();
        if (GameInput.inputInstance.Jumped() && isPlayerGrounded)
            Jump();
        //Crouch();
        IsWalking();
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * inputVector.y + orientation.right * inputVector.x;

        // Determine if the player is sprinting
        isSprinting = isPlayerGrounded && GameInput.inputInstance.IsSprinting() && !GameInput.inputInstance.IsCrouching();

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
        //playerAnimator.SetFloat("IdleToRun", currentSpeed);

        // Update player with the force of gravity 
        
            yVelocity.y += gravity * Time.deltaTime;
            characterController.Move(yVelocity * Time.deltaTime);
        

    }

    private void Jump()
    {
        yVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }





    public bool IsWalking()
    {
        return isWalking;
    }



    // Ground Checking
    private void GroundChecking()
    {
        isPlayerGrounded = characterController.isGrounded;

        if (characterController.isGrounded && yVelocity.y < 0)
        {
            yVelocity.y = -0.5f;
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

    public void DefaultMovementStats()
    {
        moveSpeed = DEFAULT_MOVE_SPEED;
        sprintSpeed = DEFAULT_SPRINT_SPEED;
        jumpForce = DEFAULT_JUMP_FORCE;
    }
}
