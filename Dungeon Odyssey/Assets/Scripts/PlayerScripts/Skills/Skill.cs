using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    [Header("Skill Information")]
    public Sprite skillIcon;
    public string skillName;
    [Header("Skill Type")]
    public bool movement;
    public bool combat;
    public bool passive;
}
