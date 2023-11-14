using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    PlayerAnimationHandler animationHandler;
    private PlayerAttackHandler playerAttackHandler;
    private PlayerInventory inventory;

    private void Awake()
    {
        animationHandler = GetComponent<PlayerAnimationHandler>();
        playerAttackHandler = GameObject.Find("Player").GetComponent<PlayerAttackHandler>();
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        HandleAttacks();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon) 
    { 
        animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1 , true);
    }

    private void HandleAttacks()
    {
        if (GameInput.inputInstance.lightattack_Input)
        {
            playerAttackHandler.HandleLightAttack(inventory.currentWeapon);
        }
        if (GameInput.inputInstance.heavyattack_Input)
        {
            playerAttackHandler.HandleHeavyAttack(inventory.currentWeapon);
        }
    }
}

