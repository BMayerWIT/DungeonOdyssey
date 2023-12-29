using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{

    private PlayerManager playerManager;
    private GameObject playerObject;
    private PlayerAttackHandler playerAttackHandler;
    public static GameInput inputInstance;
    string currentSceneName;

    private PlayerInput _inputActions;
    private InputAction _movement;
    private InputAction _mouse;
    private InputAction _jump;
    private InputAction _crouch;
    private InputAction _lightAttack;
    private InputAction _interact;
    private InputAction _decreaseHealth;
    private InputAction _pause;
    private InputAction _sprint;
    private InputAction _mouseX;
    private InputAction _mouseY;
    private InputAction _skill1;
    private InputAction _skill2;
    private InputAction _skill3;


    Vector2 inputVector;
    float mouseX;
    float mouseY;
    bool jumped;
    bool sprinting;
    bool crouching;
    bool interact;
    bool shouldDecreaseHealth;
    bool pauseState;
    bool skill1;
    bool skill2;
    bool skill3;
    Vector2 mouseVector;


    public bool comboFlag;
    public bool isInteracting;

    public bool isLightAttackingRH;
    public bool lightattack_Input;


    private void Awake()
    {
        if (inputInstance == null)
        {
            inputInstance = this;
        }

        _inputActions = GetComponent<PlayerInput>();
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Menu")
        {
            playerObject = GameObject.Find("Player");
            playerManager = playerObject.GetComponent<PlayerManager>();
            playerAttackHandler = playerObject.GetComponent<PlayerAttackHandler>();
        }

        SetUpInputActions();
    }

    private void SetUpInputActions()
    {
        _movement = _inputActions.actions["Movement"];
        _mouse = _inputActions.actions["Camera"];
        _jump = _inputActions.actions["Jump"];
        _crouch = _inputActions.actions["Crouch"];
        _lightAttack = _inputActions.actions["lightAttack"];
        _interact = _inputActions.actions["Interact"];
        _decreaseHealth = _inputActions.actions["DecreaseHealth"];
        _pause = _inputActions.actions["Pause"];
        _sprint = _inputActions.actions["Sprint"];
        _mouseX = _inputActions.actions["MouseX"];
        _mouseY = _inputActions.actions["MouseY"];
        _skill1 = _inputActions.actions["UseSkill1"];
        _skill2 = _inputActions.actions["UseSkill2"];
        _skill3 = _inputActions.actions["UseSkill3"];

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
        shouldDecreaseHealth = _decreaseHealth.WasPressedThisFrame();
        lightattack_Input = _lightAttack.WasPressedThisFrame();
        skill1 = _skill1.WasPressedThisFrame();
        skill2 = _skill2.WasPressedThisFrame();
        skill3 = _skill3.WasPressedThisFrame();
        
        
        mouseVector = _mouse.ReadValue<Vector2>();
        pauseState = _pause.WasPressedThisFrame();
        if (currentSceneName != "Menu")
        {
            LightAttack();
        }
       
        

    }
    public void TickInput(float delta)
    {
        //HandleAttackInput(delta);
        
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

    public void LightAttack()
    {

        if (lightattack_Input)
        {
            
            if (playerManager.canDoCombo)
            {
                
                comboFlag = true;
                playerAttackHandler.HandleWeaponCombo(PlayerInventory.Instance.currentWeapon);
                comboFlag = false;
            }
            else
            {
                if (playerManager.canDoCombo)
                {
                    return;
                }
                
                
                playerAttackHandler.HandleLightAttack(PlayerInventory.Instance.currentWeapon);
                
            }
        }
    }

    public bool SkillInput1()
    {
        return skill1;
    }

    public bool SkillInput2()
    {
        return skill2;
    }

    public bool SkillInput3()
    {
        return skill3;
    }

    public string ReturnSkillBindingStrings(int slotNumber)
    {
        string binding = "";
        if (slotNumber == 1)
        {
            binding = _skill1.GetBindingDisplayString();
        }
        else if (slotNumber == 2)
        {
            binding = _skill2.GetBindingDisplayString();
        }
        else if (slotNumber == 3)
        {
            binding = _skill3.GetBindingDisplayString();
        }
        else
        {
            binding = "";
        }

        return binding;
    }
}
