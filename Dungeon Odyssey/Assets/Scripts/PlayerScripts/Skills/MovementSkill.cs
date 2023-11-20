using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/MovementSkill")]
public class MovementSkill : Skill
{
    [Header("Skill Type: Movement")]
    [HideInInspector]
    public const bool TYPEMOVEMENT = true;
    [Header("Skill Attributes")]
    public int sprintSpeedBaseIncrease;
    public int walkSpeedBaseIncrease;
    public int sprintSpeedMultiplier;
    public int walkSpeedMultiplier;
    public int accelerationIncrease;
    public int staminaRegainSpeed;
    public int skillDuration;
    public int skillCooldown;
}
