using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSkillUnlock : MonoBehaviour
{
    private SkillButton skillButton;
    private Skill finalSkill;
    public GameObject OutlineLit;
    public GameObject OutlineUnlit;
    void Start()
    {
        skillButton = GetComponent<SkillButton>();
        finalSkill = skillButton.skill;
    }

    // Update is called once per frame
    void Update()
    {
        if (finalSkill.isUnlocked)
        {
            OutlineLit.SetActive(true);
            OutlineUnlit.SetActive(false);
        }
        else
        {
            OutlineLit.SetActive(false);
            OutlineUnlit.SetActive(true);
        }
    }
}
