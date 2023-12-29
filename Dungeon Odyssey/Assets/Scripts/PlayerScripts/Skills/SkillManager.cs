using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillManager : MonoBehaviour
{
    string currentSceneName;
    public int skillPoints = 5;
    public static SkillManager instance;
    private GameObject player;
    private PlayerLocomotion movementStats;


    [Header("Selected Skills")]
    public Skill[] selectedSkills;
    

    [Header("Image Objects And Sliders")]
    public List<Image> skillSprites = new List<Image>();
    public List<Image> skillSpriteOutlines= new  List<Image>();
    public List<Slider> skillSliders = new List<Slider>();
    public List<TextMeshProUGUI> skillBindingText = new List<TextMeshProUGUI>();
    private bool skill1Activated;
    private bool skill2Activated;
    private bool skill3Activated;
    public bool cooldown1;
    public bool cooldown2;
    public bool cooldown3;

    private float skill1LastActivatedTime = -20;
    private float skill2LastActivatedTime = -20;
    private float skill3LastActivatedTime = -20;

    
    public Ability abilitySlot1;
    public Ability abilitySlot2;
    public Ability abilitySlot3;
    public MovementSkill movementSkillSlot1;
    public MovementSkill movementSkillSlot2;
    public MovementSkill movementSkillSlot3;


    private void Awake()
    {
        if (instance == null)
        instance = this;
    }


    private void Start()
    {
        
        selectedSkills = SaveAndLoad.instance.skillSlotData.savedSkills;
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName != "Menu")
        {
            HandleSkillTypes();
            for (int i = 0; i < selectedSkills.Length; i++)
            {
                skillSprites[i].sprite = selectedSkills[i].skillIcon;
                skillSpriteOutlines[i].enabled = false;

            }

            player = GameObject.FindGameObjectWithTag("Player");
            movementStats = player.GetComponent<PlayerLocomotion>();
        }
        
        
        
    }

    private void Update()
    {
        if (currentSceneName != "Menu")
        {
            
            UpdateBindingText();
            UseSkill1();
            UseSkill2();
            UseSkill3();
            UpdateCooldownSlider1();
            UpdateCooldownSlider2();
            UpdateCooldownSlider3();
        }
        
    }


    #region SKILLONE
    private void UseSkill1()
    {

        // Check if the skill is not in cooldown
        if (!cooldown1)
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill1LastActivatedTime >= selectedSkills[0].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput1() && !skill1Activated)
                {
                    skill1Activated = true;
                    skillSpriteOutlines[0].enabled = true;
                    skill1LastActivatedTime = Time.time;
                    if (abilitySlot1 != null)
                    {
                        HandleMovementAbility(abilitySlot1);
                    }
                    else if (movementSkillSlot1 != null)
                    {
                        print("DASHING");
                        HandleDashSkill(movementSkillSlot1);
                    }
                    StartCoroutine(DeactivateSkillAfterDuration1(selectedSkills[0]));
                }
            }
        }   
    }

    private IEnumerator DeactivateSkillAfterDuration1(Skill skill)
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(skill.skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[0].enabled = false;
        DeactivateSkill1();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate1(skill));
    }

    private IEnumerator StartCooldownAndSliderUpdate1(Skill skill)
    {
        cooldown1 = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(skill.skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill1Activated = false;
        cooldown1 = false;

        
        
    }

    private void DeactivateSkill1()
    {
        if (abilitySlot1 != null)
        {
            RevertSkillChanges(abilitySlot1);
        }
    }

    private void UpdateCooldownSlider1()
    {
        if (cooldown1)
        {

            float elapsedTime = Time.time - (skill1LastActivatedTime + selectedSkills[0].skillDuration);
            
            float cooldownProgress = Mathf.Clamp01(1 - elapsedTime / selectedSkills[0].skillCooldown);
            skillSliders[0].value = cooldownProgress; // Adjusted for correct progress representation
            Debug.Log(cooldownProgress);
        }
        else
        {
            // Skill not activated, reset slider
            skillSliders[0].value = 0f;
        }
    }

    #endregion

    #region SKILLTWO
    private void UseSkill2()
    {

        // Check if the skill is not in cooldown
        if (!cooldown2)
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill2LastActivatedTime >= selectedSkills[1].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput2() && !skill2Activated)
                {
                    skill2Activated = true;
                    skillSpriteOutlines[1].enabled = true;
                    skill2LastActivatedTime = Time.time;
                    if (abilitySlot2 != null)
                    {
                        HandleMovementAbility(abilitySlot2);
                    }
                    else if (movementSkillSlot2 != null)
                    {
                        print("DASHING");
                        HandleDashSkill(movementSkillSlot2);
                    }
                    StartCoroutine(DeactivateSkillAfterDuration2(selectedSkills[1]));
                }
            }
        }
    }

    private IEnumerator DeactivateSkillAfterDuration2(Skill skill)
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(skill.skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[1].enabled = false;
        DeactivateSkill2();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate2(skill));
    }

    private IEnumerator StartCooldownAndSliderUpdate2(Skill skill)
    {
        cooldown2 = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(skill.skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill2Activated = false;
        cooldown2 = false;



    }

    private void DeactivateSkill2()
    {
        if (abilitySlot2 != null);
        {
            RevertSkillChanges(abilitySlot2);
        }
    }

    private void UpdateCooldownSlider2()
    {
        if (cooldown2)
        {

            float elapsedTime = Time.time - (skill2LastActivatedTime + selectedSkills[1].skillDuration);
            Debug.Log(elapsedTime);
            float cooldownProgress = Mathf.Clamp01(1 - elapsedTime / (selectedSkills[1].skillCooldown));
            skillSliders[1].value = cooldownProgress; // Adjusted for correct progress representation
        }
        else
        {
            // Skill not activated, reset slider
            skillSliders[1].value = 0f;
        }
    }

    #endregion

    #region SKILLTHREE
    private void UseSkill3()
    {

        // Check if the skill is not in cooldown
        if (!cooldown3)
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill1LastActivatedTime >= selectedSkills[2].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput3() && !skill3Activated)
                {
                    skill3Activated = true;
                    skillSpriteOutlines[2].enabled = true;
                    skill3LastActivatedTime = Time.time;
                    if (abilitySlot3 != null)
                    {
                        HandleMovementAbility(abilitySlot3);
                    }
                    else if (movementSkillSlot3 != null)
                    {
                        print("DASHING");
                        HandleDashSkill(movementSkillSlot3);
                    }
                    StartCoroutine(DeactivateSkillAfterDuration3(selectedSkills[2]));
                }
            }
        }
    }

    private IEnumerator DeactivateSkillAfterDuration3(Skill skill)
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(skill.skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[2].enabled = false;
        DeactivateSkill3();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate3(skill));
    }

    private IEnumerator StartCooldownAndSliderUpdate3(Skill skill)
    {
        cooldown3 = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(skill.skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill3Activated = false;
        cooldown3 = false;



    }

    private void DeactivateSkill3()
    {
        if (abilitySlot3 != null)
        {
            RevertSkillChanges(abilitySlot3);
        }
    }

    private void UpdateCooldownSlider3()
    {
        if (cooldown3)
        {

            float elapsedTime = Time.time - (skill3LastActivatedTime + selectedSkills[2].skillDuration);

            float cooldownProgress = Mathf.Clamp01(1 - elapsedTime / selectedSkills[2].skillCooldown);
            skillSliders[2].value = cooldownProgress; // Adjusted for correct progress representation
            Debug.Log(cooldownProgress);
        }
        else
        {
            // Skill not activated, reset slider
            skillSliders[2].value = 0f;
        }
    }

    #endregion

    private void HandleSkillTypes()
    {


        if (selectedSkills[0] is Ability)
        {
            Ability skill = (Ability)selectedSkills[0];
            abilitySlot1 = skill;
        }
        else if (selectedSkills[0] is MovementSkill)
        {
            MovementSkill skill = (MovementSkill)selectedSkills[0];
            movementSkillSlot1 = skill;
        }

        if (selectedSkills[1] is Ability)
        {
            Ability skill = (Ability)selectedSkills[1];
            abilitySlot2 = skill;
        }
        else if (selectedSkills[1] is MovementSkill)
        {
            MovementSkill skill = (MovementSkill)selectedSkills[1];
            movementSkillSlot2 = skill;
        }

        if (selectedSkills[2] is Ability)
        {
            Ability skill = (Ability)selectedSkills[2];
            abilitySlot3 = skill;
        }
        else if (selectedSkills[2] is MovementSkill)
        {
            MovementSkill skill = (MovementSkill)selectedSkills[2];
            movementSkillSlot3 = skill;
        }

    }


private void HandleMovementAbility(Ability skill)
    {
        

        if (skill != null)
        {
            // Apply skill effects
            movementStats.sprintSpeed += skill.sprintSpeedBaseIncrease;
            movementStats.moveSpeed += skill.walkSpeedBaseIncrease;
            movementStats.moveSpeed *= skill.walkSpeedMultiplier;
            movementStats.sprintSpeed *= skill.sprintSpeedMultiplier;
        }
    }

    private void RevertSkillChanges(Ability skill)
    {
        // Revert the changes made by the skill
        movementStats.moveSpeed /= skill.walkSpeedMultiplier;
        movementStats.sprintSpeed /= skill.sprintSpeedMultiplier;
        movementStats.moveSpeed -= skill.walkSpeedBaseIncrease;
        movementStats.sprintSpeed -= skill.sprintSpeedBaseIncrease;
    }

    private void HandleDashSkill(MovementSkill skill)
    {
        if (skill.isDash == true)
        { 
            PlayerLocomotion.Instance.StartDash();
        }
        
    }
     
    public void DeductSkillPoints(int cost)
    {
        skillPoints -= cost;
    }

    private void UpdateBindingText()
    {
        skillBindingText[0].text = GameInput.inputInstance.ReturnSkillBindingStrings(1);
        skillBindingText[1].text = GameInput.inputInstance.ReturnSkillBindingStrings(2);
        skillBindingText[2].text = GameInput.inputInstance.ReturnSkillBindingStrings(3);    }
}
