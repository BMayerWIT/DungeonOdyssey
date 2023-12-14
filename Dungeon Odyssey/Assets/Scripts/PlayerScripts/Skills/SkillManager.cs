using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool skill1Activated;
    private bool skill2Activated;
    public bool cooldown1;
    public bool cooldown2;

    private float skill1LastActivatedTime = -20;
    private float skill2LastActivatedTime = -20;

    public List<Ability> skillSlots = new List<Ability>();

    private void Awake()
    {
        if (instance == null)
        instance = this;
    }


    private void Start()
    {
        selectedSkills = new Skill[3];
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
            UseSkill1();
            UseSkill2();
            UpdateCooldownSlider1();
            UpdateCooldownSlider2();
        }
        
    }


    #region SKILLONE
    private void UseSkill1()
    {
        // Check if the skill is not in cooldown
        if (!cooldown1)
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill1LastActivatedTime >= skillSlots[0].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput1() && !skill1Activated)
                {
                    // Activate the skill
                    skill1Activated = true;
                    skillSpriteOutlines[0].enabled = true;
                    // Store the activation time
                    skill1LastActivatedTime = Time.time;
                    HandleMovementSkill(skillSlots[0]);
                    // Deactivate the skill after a certain duration
                    StartCoroutine(DeactivateSkillAfterDuration1());
                }
            }
        }
    }

    private IEnumerator DeactivateSkillAfterDuration1()
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(skillSlots[0].skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[0].enabled = false;
        DeactivateSkill1();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate1());
    }

    private IEnumerator StartCooldownAndSliderUpdate1()
    {
        cooldown1 = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(skillSlots[0].skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill1Activated = false;
        cooldown1 = false;

        
        
    }

    private void DeactivateSkill1()
    {
        if (skillSlots[0] != null)
        {
            RevertSkillChanges(skillSlots[0]);
        }
    }

    private void UpdateCooldownSlider1()
    {
        if (cooldown1)
        {

            float elapsedTime = Time.time - (skill1LastActivatedTime + skillSlots[0].skillDuration);
            Debug.Log(elapsedTime);
            float cooldownProgress = Mathf.Clamp01(elapsedTime / (skillSlots[0].skillCooldown));
            skillSliders[0].value = cooldownProgress; // Adjusted for correct progress representation
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
            if (Time.time - skill2LastActivatedTime >= skillSlots[1].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput2() && !skill2Activated)
                {
                    // Activate the skill
                    skill2Activated = true;
                    skillSpriteOutlines[1].enabled = true;
                    // Store the activation time
                    skill2LastActivatedTime = Time.time;
                    HandleMovementSkill(skillSlots[1]);
                    // Deactivate the skill after a certain duration
                    StartCoroutine(DeactivateSkillAfterDuration2());
                }
            }
        }
    }

    private IEnumerator DeactivateSkillAfterDuration2()
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(skillSlots[1].skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[1].enabled = false;
        DeactivateSkill2();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate2());
    }

    private IEnumerator StartCooldownAndSliderUpdate2()
    {
        cooldown2 = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(skillSlots[1].skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill2Activated = false;
        cooldown2 = false;



    }

    private void DeactivateSkill2()
    {
        if (skillSlots[1] != null);
        {
            RevertSkillChanges(skillSlots[1]);
        }
    }

    private void UpdateCooldownSlider2()
    {
        if (cooldown2)
        {

            float elapsedTime = Time.time - (skill2LastActivatedTime + skillSlots[1].skillDuration);
            Debug.Log(elapsedTime);
            float cooldownProgress = Mathf.Clamp01(elapsedTime / (skillSlots[1].skillCooldown));
            skillSliders[1].value = cooldownProgress; // Adjusted for correct progress representation
        }
        else
        {
            // Skill not activated, reset slider
            skillSliders[1].value = 0f;
        }
    }

    #endregion

    private void HandleSkillTypes()
    {
        for (int i = 0; i < selectedSkills.Length; i++)
        {

            if (selectedSkills[i] is Ability)
            {
                Ability skill = (Ability)selectedSkills[i];
                skillSlots.Add(skill);
            } 
        }

    }


private void HandleMovementSkill(Ability skill)
    {
        

        if (skillSlots[0] != null)
        {
            // Apply skill effects
            movementStats.sprintSpeed += skillSlots[0].sprintSpeedBaseIncrease;
            movementStats.moveSpeed += skillSlots[0].walkSpeedBaseIncrease;
            movementStats.moveSpeed *= skillSlots[0].walkSpeedMultiplier;
            movementStats.sprintSpeed *= skillSlots[0].sprintSpeedMultiplier;
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
     
    public void DeductSkillPoints(int cost)
    {
        skillPoints -= cost;
    }
}
