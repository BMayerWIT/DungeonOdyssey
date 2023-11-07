using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    PlayerAnimationHandler animationHandler;

    private void Awake()
    {
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon) 
    { 
        animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1 , true);
    }
}

