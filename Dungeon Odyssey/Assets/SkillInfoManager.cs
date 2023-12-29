using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SkillInfoManager : MonoBehaviour
{
    public static SkillInfoManager instance;

    [Header("Current Selected Skill")]
    public Skill currentSkill;
    public Image currentSkillImage;
    public Image outline;
    public Skill currentSkillToBeEquipped;
    public bool currentSkillIsUnlockable;

    [Header("Skill Information")]
    public GameObject equipButton;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI buttonText;

    [Header("UI Elements")]
    public GameObject[] EquippingUI;
    public GameObject movementSkillTreeUI;

    [Header("Skill Slots")]
    public Image Slot1Image;
    public Image Slot2Image;
    public Image Slot3Image;

    private Skill slot1Skill;
    private Skill slot2Skill;
    private Skill slot3Skill;
    private Skill[] loadedSkills;
    private bool equippingSkillFlag;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        LoadEquippedSkillsOnSlots();
    }

    private void Update()
    {
        HandleSkillEquipUI(equippingSkillFlag);
        if (currentSkill != null)
        {
            if (currentSkill is Skill)
            {
                
                EnableUI();
                LoadSkillInfo(currentSkill.skillName, currentSkill.description, currentSkill.skillDuration, currentSkill.skillCooldown, currentSkill.skillIcon, currentSkill.Cost);
            }
        }
        else
        {
            DisableUI();
        }
    }

    private void LoadSkillInfo(string name, string description, float duration, float cooldown, Sprite icon, int cost)
    {
        
        skillName.text = name;
        descriptionText.text = description;
        durationText.text = "Duration: " + duration.ToString() + "s";
        cooldownText.text = "Cooldown: " + cooldown.ToString() + "s";
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
        if (!currentSkill.isUnlocked)
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
        
        
        if (currentSkill.isUnlocked)
        {

            AudioManager.Instance.equipFlag = true;
            currentSkillToBeEquipped = currentSkill;
            equippingSkillFlag = true;
        }
        else
        {
            AudioManager.Instance.equipFlag = false;
            currentSkillToBeEquipped = null;
            equippingSkillFlag = false;
        }

        if (currentSkill.isUnlockable && !currentSkill.isUnlocked)
        {
            if (SkillManager.instance.skillPoints >= currentSkill.Cost)
            {
                currentSkill.isUnlocked = true;
                costText.enabled = false;
                SkillManager.instance.DeductSkillPoints(currentSkill.Cost);
                AudioManager.Instance.PlayUnlockSkillSound();
            }
        }
        else
        {
            AudioManager.Instance.PlayButtonSound();
        }


    }

    private string LoadEquipButtonText()
    {
        string text;
        if (currentSkill.isUnlocked)
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
        if (equippingSkillFlag)
        {
            if (currentSkill != slot2Skill && currentSkill != slot3Skill)
            {
                slot1Skill = currentSkill;
                SkillManager.instance.selectedSkills[0] = slot1Skill;
                SaveAndLoad.instance.skillSlotData.savedSkills[0] = slot1Skill;
                Slot1Image.sprite = slot1Skill.skillIcon;
                equippingSkillFlag = false;
                
            }
            else
            {
                equippingSkillFlag = false;
                print("tried but failed which is good");
            }
        }
    }
    
    public void EquipSlot2()
    {
        if (equippingSkillFlag)
        { 
            if (currentSkill != slot1Skill && currentSkill != slot3Skill)
            {
                slot2Skill = currentSkill;
                SkillManager.instance.selectedSkills[1] = slot2Skill;
                SaveAndLoad.instance.skillSlotData.savedSkills[1] = slot2Skill;
                Slot2Image.sprite = slot2Skill.skillIcon;
                equippingSkillFlag = false;
                
            }
            else
            {
                equippingSkillFlag = false;
                print("tried but failed which is good");
            }
        }
    }
    
    public void EquipSlot3()
    {
        if (equippingSkillFlag)
        {
            if (currentSkill != slot1Skill && currentSkill != slot2Skill)
            {
                slot3Skill = currentSkill;
                SkillManager.instance.selectedSkills[2] = slot3Skill;
                SaveAndLoad.instance.skillSlotData.savedSkills[2] = slot3Skill;
                Slot3Image.sprite = slot3Skill.skillIcon;
                equippingSkillFlag = false;
                
            }
            else
            {
                equippingSkillFlag = false;
                print("tried but failed which is good");
            }
        }
    }

    private void LoadEquippedSkillsOnSlots()
    {
        loadedSkills = SaveAndLoad.instance.skillSlotData.savedSkills;

        slot1Skill = loadedSkills[0];
        slot2Skill = loadedSkills[1];
        slot3Skill = loadedSkills[2];

        Slot1Image.sprite = slot1Skill.skillIcon;
        Slot2Image.sprite = slot2Skill.skillIcon;
        Slot3Image.sprite = slot3Skill.skillIcon;

    }



    private void HandleSkillEquipUI(bool equipping)
    {
        if (equipping)
        {
            movementSkillTreeUI.SetActive(false);

            for (int i = 0; i < EquippingUI.Count(); i++)
            {
                EquippingUI[i].SetActive(true);
            }
            
            
        }
        else
        {
            movementSkillTreeUI.SetActive(true);
            for (int i = 0; i < EquippingUI.Count(); i++)
            {
                EquippingUI[i].SetActive(false);
            }
        }
    }
}

