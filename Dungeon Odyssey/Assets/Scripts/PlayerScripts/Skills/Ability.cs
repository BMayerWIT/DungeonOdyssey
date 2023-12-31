using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Ability")]
public class Ability : Skill
{
    
    public enum abilityType
    {
        Movement,
        Combat,
        Attack
    }

    [Header("Movement Skill Attributes")]
    public int sprintSpeedBaseIncrease;
    public int walkSpeedBaseIncrease;
    public float sprintSpeedMultiplier;
    public float walkSpeedMultiplier;
    public int accelerationIncrease;
    public float staminaRegainSpeed;
    public float staminaDrainSpeed;
    



    [Header("Combat Skill Attributes")]
    public int damageBaseIncrease;
    public int damageMultiplier;
    public int criticalHitPercent;
}
