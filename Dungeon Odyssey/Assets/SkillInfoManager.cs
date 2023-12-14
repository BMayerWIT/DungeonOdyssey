using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SkillInfoManager : MonoBehaviour
{
    public static SkillInfoManager instance;
    public Skill currentSkill;
    public Skill currentSkillToBeEquipped;
    public bool currentSkillIsUnlockable;
    public Ability currentAbility;
    public Image currentSkillImage;
    public Image outline;
    public GameObject equipButton;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI buttonText;
    public Image Slot1Image;
    public Image Slot2Image;
    public Image Slot3Image;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {

        if (currentSkill != null)
        {
            if (currentSkill is Ability)
            {
                currentAbility = (Ability)currentSkill;
                EnableUI();
                LoadSkillInfo(currentAbility.skillName, currentAbility.description, currentAbility.skillDuration, currentAbility.skillCooldown, currentAbility.skillIcon, currentAbility.Cost);
            }
        }
        else
        {
            DisableUI();
        }
    }

    private void LoadSkillInfo(string name, string description, int duration, int cooldown, Sprite icon, int cost)
    {
        
        skillName.text = name;
        descriptionText.text = "\"" + description + "\"";
        durationText.text = "Duration: " + duration.ToString();
        cooldownText.text = "Cooldown: " + cooldown.ToString();
        costText.text = "Cost: " + cost.ToString();
        currentSkillImage.sprite = icon;
        buttonText.text = LoadEquipButtonText();
    }

    private void EnableUI()
    {
        equipButton.SetActive(true);
        outline.enabled = true;
        skillName.enabled = true;
        descriptionText.enabled = true;
        durationText.enabled = true;
        cooldownText.enabled = true;
        currentSkillImage.enabled = true;
        if (!currentAbility.isUnlocked)
        {
            costText.enabled = true;
        }
        else
        {
            costText.enabled = false;
        }
        
    }

    private void DisableUI()
    {
        equipButton.SetActive(false);
        outline.enabled = false;
        skillName.enabled = false;
        descriptionText.enabled = false;
        durationText.enabled = false;
        cooldownText.enabled = false;
        currentSkillImage.enabled = false;
        costText.enabled= false;
    }

    public void UnlockSkill()
    {
        if (currentSkill.isUnlockable)
        {
            if (SkillManager.instance.skillPoints >= currentSkill.Cost)
            {
                currentSkill.isUnlocked = true;
                costText.enabled = false;
                SkillManager.instance.DeductSkillPoints(currentSkill.Cost);
            }
        }
        else if (currentSkill.isUnlockable)
        {
            currentSkill.isUnlocked = true;
            costText.enabled = false;
            SkillManager.instance.DeductSkillPoints(currentSkill.Cost);
        }
            
    }

    private string LoadEquipButtonText()
    {
        string text;
        if (currentAbility.isUnlocked)
        {
            text = "Equip";
        }
        else
        {
            text = "Unlock";
        }
        return text;
    }

    public void EquipSlot1()
    {
        SkillManager.instance.selectedSkills[0] = currentSkill;
        Slot1Image.sprite = currentSkill.skillIcon;
    }
    
    public void EquipSlot2()
    {
        SkillManager.instance.selectedSkills[1] = currentSkill;
        Slot2Image.sprite = currentSkill.skillIcon;
    }
    
    public void EquipSlot3()
    {
        SkillManager.instance.selectedSkills[2] = currentSkill;
        Slot3Image.sprite = currentSkill.skillIcon;
    }
}

