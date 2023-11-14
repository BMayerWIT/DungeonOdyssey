using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput inputInstance;

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
    private InputAction _mouseX;
    private InputAction _mouseY;

    Vector2 inputVector;
    float mouseX;
    float mouseY;
    bool jumped;
    bool sprinting;
    bool crouching;
    bool interact;
    bool toggleCam;
    bool shouldDecreaseHealth;
    bool dashInput;
    bool pauseState;
    Vector2 mouseVector;

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
        _dash = _inputActions.actions["Dash"];
        _toggleCamera = _inputActions.actions["ToggleCamera"];
        _decreaseHealth = _inputActions.actions["DecreaseHealth"];
        _pause = _inputActions.actions["Pause"];
        _sprint = _inputActions.actions["Sprint"];
        _mouseX = _inputActions.actions["MouseX"];
        _mouseY = _inputActions.actions["MouseY"];

    }

    private void Update()
    {
        float delta = Time.fixedDeltaTime;
        inputVector = _movement.ReadValue<Vector2>();
        mouseX = _mouseX.ReadValue<float>() * Time.deltaTime;
        mouseY = _mouseY.ReadValue<float>() * Time.deltaTime;
        jumped = _jump.WasPressedThisFrame();
        sprinting = _sprint.IsPressed();
        crouching = _crouch.IsPressed();
        interact = _interact.WasPressedThisFrame();
        toggleCam = _toggleCamera.WasPressedThisFrame();
        shouldDecreaseHealth = _decreaseHealth.WasPressedThisFrame();
        dashInput = _dash.WasPressedThisFrame();
        _lightAttack.performed += i => lightattack_Input = true;
        _heavyAttack.performed += i => heavyattack_Input = true;
        mouseVector = _mouse.ReadValue<Vector2>();
        pauseState = _pause.WasPressedThisFrame();
        
    }
    public void TickInput(float delta)
    {
        HandleAttackInput(delta);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public float GetMouseX()
    {
        return mouseX;
    }

    public float GetMouseY()
    {
        return mouseY;
    }

    public bool Jumped()
    {
        return jumped;
    }

    public bool IsSprinting()
    {
        return sprinting;
    }

    public bool IsCrouching()
    {
        return crouching;
    }

    public bool Interacting()
    {
        return interact;
    }

    public bool GetToggleCamera()
    {
        return toggleCam;
    }

    public bool DecreaseHealth()
    {
        return shouldDecreaseHealth;
    }

    public Vector2 GetMouseMovement()
    {
        
        return mouseVector;
    }

    public bool ReturnPauseState()
    {
        return pauseState;
    }

    public bool HandleDashInput()
    {
        if (dashInput)
            dashFlag = true;
        else
            dashFlag = false;

        return dashInput;
    }



    private void HandleAttackInput(float delta)
    {
        
    }
}
