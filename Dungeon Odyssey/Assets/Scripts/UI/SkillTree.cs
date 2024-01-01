using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public TextMeshProUGUI skillPointText;
    private void Update()
    {
        skillPointText.text = "Skill Points: " + StatsHandler.Instance.skillPoints.ToString();
    }
}
