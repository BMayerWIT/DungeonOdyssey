using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput inputInstance;
    
    private PlayerAttackHandler playerAttackHandler;
    private PlayerInventory inventory;

    private PlayerInput _inputActions;
    private InputAction _movement;
    private InputAction _mouse;
    private InputAction _jump;
    private InputAction _crouch;
    private InputAction _lightAttack;
    private InputAction _heavyAttack;
    private InputAction _interact;
    private InputAction _dash;
    private InputAction _toggleCamera;
    private InputAction _decreaseHealth;
    private InputAction _pause;
    private InputAction _sprint;


    public bool dashFlag = false;
    public bool isInteracting;
    
    public bool lightattack_Input;
    public bool heavyattack_Input;

    private void Awake()
    {
        if (inputInstance == null)
        {
            inputInstance = this;
        }
        playerAttackHandler = GameObject.Find("Player").GetComponent<PlayerAttackHandler>();
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        _inputActions = GetComponent<PlayerInput>();

        SetUpInputActions();
    }

    private void SetUpInputActions()
    {
        _movement = _inputActions.actions["Movement"];
        _mouse = _inputActions.actions["Camera"];
        _jump = _inputActions.actions["Jump"];
        _crouch = _inputActions.actions["Crouch"];
        _lightAttack = _inputActions.actions["lightAttack"];
        _heavyAttack = _inputActions.actions["heavyAttack"];
        _interact = _inputActions.actions["Interact"];
        _dash= _inputActions.actions["Dash"];
        _toggleCamera = _inputActions.actions["ToggleCamera"];
        _decreaseHealth = _inputActions.actions["DecreaseHealth"];
        _pause = _inputActions.actions["Pause"];
        _sprint = _inputActions.actions["Sprint"];

    }

    public void TickInput(float delta)
    {
        HandleAttackInput(delta);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    //public float GetMouseX()
    //{
    //    float mouseX = playerInputActions.Player.MouseX.ReadValue<float>() * Time.deltaTime;
    //    return mouseX;
    //}

    //public float GetMouseY()
    //{
    //    float mouseY = playerInputActions.Player.MouseY.ReadValue<float>() * Time.deltaTime;
    //    return mouseY;
    //}

    public bool Jumped()
    {
        bool Jumped = _jump.WasPressedThisFrame();
        return Jumped;
    }

    public bool IsSprinting()
    {
        bool sprinting = _sprint.IsPressed();
        return sprinting;
    }

    public bool IsCrouching()
    {
        bool crouching = _crouch.IsPressed();
        return crouching;
    }

    public bool Interacting()
    {
        bool interact = _interact.WasPressedThisFrame();
        return interact;
    }

    public bool GetToggleCamera()
    {
        bool toggleCam = _toggleCamera.WasPressedThisFrame();
        return toggleCam;
    }

    public bool DecreaseHealth()
    {
        bool shouldDecreaseHealth = _decreaseHealth.WasPressedThisFrame();
        return shouldDecreaseHealth;
    }

    public Vector2 GetMouseMovement()
    {
        Vector2 mouseVector = _mouse.ReadValue<Vector2>();
        return mouseVector;
    }

    public bool HandleDashInput()
    {
        
        bool dashInput = _dash.WasPressedThisFrame();
        dashFlag = true;
        return dashInput;
    }

   

    private void HandleAttackInput(float delta)
    {
        _lightAttack.performed += i => lightattack_Input = true;
        _heavyAttack.performed += i => heavyattack_Input = true;

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
