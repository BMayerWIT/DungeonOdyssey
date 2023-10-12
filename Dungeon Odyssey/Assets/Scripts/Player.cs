using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera playerCamera;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintSpeed = 1.5f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpForce;

    private Vector2 inputVector;
    private Vector3 moveDirection;
    private Vector3 yVelocity;
    private CharacterController characterController;

    private float globalSpeed;
    private float standingHeight;
    private bool isGrounded;
    private bool isWalking = false;
    private bool isRunning = false;

    [Header("Interacting")]
    [SerializeField] private float interactDistance;
    


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = characterController.height;
    }

    private void Update()
    {
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

        // Not pressing sprint or crouch i.e. walking
        if (isGrounded && !gameInput.IsCrouching() && !gameInput.IsSprinting())
        {
            globalSpeed = moveSpeed;

        }
        // Crouching
        else if (isGrounded && gameInput.IsCrouching())
        {
            globalSpeed = moveSpeed / 2f;
        }
        // Not crouching or walking i.e. sprinting
        else if (isGrounded && gameInput.IsSprinting() && !gameInput.IsCrouching())
        {
            globalSpeed = moveSpeed * sprintSpeed;
        }
        
        // Update player movement with speed and direction
        characterController.Move(moveDirection * globalSpeed * Time.deltaTime);

        // Update player with force of gravity 
        yVelocity.y += gravity * Time.deltaTime;
        characterController.Move(yVelocity * Time.deltaTime);
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
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
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
}