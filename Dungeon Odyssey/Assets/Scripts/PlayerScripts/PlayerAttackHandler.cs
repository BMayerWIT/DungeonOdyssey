using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    PlayerAnimationHandler animationHandler;
    public string lastAttack;
    
    private PlayerInventory inventory;

    private void Awake()
    {
        animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        
        inventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        
    }

    private void Update()
    {
        //HandleAttacks();
       // LightAttack();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (GameInput.inputInstance.comboFlag)
        {
            animationHandler.animator.SetBool("canDoCombo", false);
            if (lastAttack == weapon.ArmAttackOHRight1)
            {
                animationHandler.PlayTargetAnimation(weapon.ArmAttackOHRight2, true);
                lastAttack = weapon.ArmAttackOHRight2;
            }
            else if (lastAttack == weapon.ArmAttackOHRight2)
            {
                animationHandler.PlayTargetAnimation(weapon.ArmAttackOHRight3, true);
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {

        if (!GameInput.inputInstance.isLightAttackingRH)
        { 
            animationHandler.PlayTargetAnimation(weapon.ArmAttackOHRight1, true);
            lastAttack = weapon.ArmAttackOHRight1; 
        }
    }

    

    private void HandleAttacks()
    {
        if (GameInput.inputInstance.lightattack_Input)
        {
            HandleLightAttack(inventory.currentWeapon);
        }
       
    }

    
}

