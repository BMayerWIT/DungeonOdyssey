using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
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

    public bool Attacking()
    {
        bool attacking = playerInputActions.Player.Attack.WasPressedThisFrame();
        return attacking;
    }

    public bool Interacting()
    {
        bool interact = playerInputActions.Player.Interact.WasPressedThisFrame();
        return interact;
    }
}
