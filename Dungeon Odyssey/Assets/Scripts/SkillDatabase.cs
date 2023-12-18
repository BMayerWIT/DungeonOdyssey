using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{
    public Skill[] savedSkills = new Skill[3];
}
