using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Skill")]
public class SkillObject : Skill
{
    [Header("Skill Type")]
    [SerializeField]
    private MyEnum myEnumVariable;

   
    private bool movement;
    private bool combat;
    private bool passive;

    // Example enum
    public enum MyEnum
    {
        Movement,
        Combat,
        Passive
    }

    private void Update()
    {
        
        switch (myEnumVariable)
        {
            case MyEnum.Movement:
                movement = true;
                break;
            case MyEnum.Combat:
                combat = true;
                break;
            case MyEnum.Passive:
                passive = true;
                break;
        }
    }

}
