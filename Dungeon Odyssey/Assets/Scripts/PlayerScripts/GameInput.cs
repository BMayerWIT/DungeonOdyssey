using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput gameInput;
    public PlayerInputActions playerInputActions;
    private PlayerAttackHandler playerAttackHandler;
    private PlayerInventory inventory;

    public bool dashFlag = false;
    public bool isInteracting;
    
    public bool lightattack_Input;
    public bool heavyattack_Input;

    private void Awake()
    {
        playerAttackHandler = GameObject.Find("Player").GetComponent<PlayerAttackHandler>();
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        
        playerInputActions.Player.Enable();
        
    }

    public void TickInput(float delta)
    {
        HandleAttackInput(delta);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public float GetMouseX()
    {
        float mouseX = playerInputActions.Player.MouseX.ReadValue<float>() * Time.deltaTime;
        return mouseX;
    }

    public float GetMouseY()
    {
        float mouseY = playerInputActions.Player.MouseY.ReadValue<float>() * Time.deltaTime;
        return mouseY;
    }

    public bool Jumped()
    {
        bool Jumped = playerInputActions.Player.Jump.WasPressedThisFrame();
        return Jumped;
    }

    public bool IsSprinting()
    {
        bool sprinting = playerInputActions.Player.Sprint.IsPressed();
        return sprinting;
    }

    public bool IsCrouching()
    {
        bool crouching = playerInputActions.Player.Crouch.IsPressed();
        return crouching;
    }

    public bool Interacting()
    {
        bool interact = playerInputActions.Player.Interact.WasPressedThisFrame();
        return interact;
    }

    public bool GetToggleCamera()
    {
        bool toggleCam = playerInputActions.Player.ToggleCamera.WasPressedThisFrame();
        return toggleCam;
    }

    public bool DecreaseHealth()
    {
        bool shouldDecreaseHealth = playerInputActions.Player.DecreaseHealth.WasPressedThisFrame();
        return shouldDecreaseHealth;
    }

    public Vector2 GetMouseMovement()
    {
        Vector2 mouseVector = playerInputActions.Player.Camera.ReadValue<Vector2>();
        return mouseVector;
    }

    public bool HandleDashInput()
    {
        
        bool dashInput = playerInputActions.Player.Dash.WasPressedThisFrame();
        dashFlag = true;
        return dashInput;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void HandleAttackInput(float delta)
    {
        playerInputActions.Player.LightAttack.performed += i => lightattack_Input = true;
        playerInputActions.Player.HeavyAttack.performed += i => heavyattack_Input = true;

        if (lightattack_Input)
        {
            playerAttackHandler.HandleLightAttack(inventory.currentWeapon);
        }
        if (heavyattack_Input)
        {
            playerAttackHandler.HandleHeavyAttack(inventory.currentWeapon);
        }
    }
}
