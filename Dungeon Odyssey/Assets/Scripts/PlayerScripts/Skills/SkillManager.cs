using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    private GameObject player;
    private PlayerLocomotion movementStats;
    private bool skill1Activated;

    [Header("Selected Skills")]
    public Skill[] selectedSkills;
    

    [Header("Image Objects And Sliders")]
    public List<Image> skillSprites = new List<Image>();
    public List<Image> skillSpriteOutlines= new  List<Image>();
    public List<Slider> skillSliders = new List<Slider>();
    public List<bool> cooldownList = new List<bool>();

    private float skill1LastActivatedTime = -20;

    public List<MovementSkill> movementSkillSlot = new List<MovementSkill>();
    
    
    

    private void Start()
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

    private void Update()
    {
        
        UseSkill1();
        UpdateCooldownSlider();
    }

    

    private void UseSkill1()
    {
        // Check if the skill is not in cooldown
        if (!cooldownList[0])
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill1LastActivatedTime >= movementSkillSlot[0].skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput1() && !skill1Activated)
                {
                    // Activate the skill
                    skill1Activated = true;
                    skillSpriteOutlines[0].enabled = true;
                    // Store the activation time
                    skill1LastActivatedTime = Time.time;

                    if (movementSkillSlot[0] != null)
                    {
                        // Apply skill effects
                        movementStats.moveSpeed *= movementSkillSlot[0].walkSpeedMultiplier;

                        // Deactivate the skill after a certain duration
                        StartCoroutine(DeactivateSkillAfterDuration());
                    }
                }
            }
        }
    }

    private IEnumerator DeactivateSkillAfterDuration()
    {
        // Wait for the skill duration
        yield return new WaitForSeconds(movementSkillSlot[0].skillDuration);

        // Deactivate the skill
        skillSpriteOutlines[0].enabled = false;
        DeactivateSkill();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate());
    }

    private IEnumerator StartCooldownAndSliderUpdate()
    {
        cooldownList[0] = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(movementSkillSlot[0].skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill1Activated = false;
        cooldownList[0] = false;

        
        
    }

    private void DeactivateSkill()
    {
        if (movementSkillSlot[0] != null)
        {
            // Revert the changes made by the skill
            movementStats.moveSpeed /= movementSkillSlot[0].walkSpeedMultiplier;
        }
    }

    private void UpdateCooldownSlider()
    {
        if (cooldownList[0])
        {

            float elapsedTime = Time.time - (skill1LastActivatedTime + movementSkillSlot[0].skillDuration);
            Debug.Log(elapsedTime);
            float cooldownProgress = Mathf.Clamp01(elapsedTime / (movementSkillSlot[0].skillCooldown));
            skillSliders[0].value = cooldownProgress; // Adjusted for correct progress representation
        }
        else
        {
            // Skill not activated, reset slider
            skillSliders[0].value = 0f;
        }
    }





    private MovementSkill CheckSkillIsMovementType(Skill selectedSkill)
    {
        if (selectedSkill is MovementSkill)
        {
            MovementSkill skill = (MovementSkill)selectedSkill;
            return skill;
        }
        return null;
    }

    private void HandleSkillTypes()
    {
        for (int i = 0; i < selectedSkills.Length; i++)
        {
           
            MovementSkill validMovementSkill = CheckSkillIsMovementType(selectedSkills[i]);
            movementSkillSlot.Add(validMovementSkill);
        }

    }
     
}
