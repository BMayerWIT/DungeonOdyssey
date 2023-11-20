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
    public Skill selectedSkill1;
    public Skill selectedSkill2;
    public Skill selectedSkill3;

    [Header("Image Objects And Sliders")]
    public Image selectedImage1;
    public Image selectedImageOutline;
    public Slider skill1Slider;
    public Image selectedImage2;
    public Image selectedImage3;

    private float skill1LastActivatedTime = -20;

    private MovementSkill movementSkillSlot1 = null;
    

    private void Start()
    {

        selectedImage1.sprite = selectedSkill1.skillIcon;
        selectedImageOutline.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        movementStats = player.GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        movementSkillSlot1 = CheckSkillIsMovementType(selectedSkill1);
        UseSkill1();
        UpdateCooldownSlider();
        
    }

    private bool isCooldownActive = false;

    private void UseSkill1()
    {
        // Check if the skill is not in cooldown
        if (!isCooldownActive)
        {
            // Check if enough time has passed since the last activation
            if (Time.time - skill1LastActivatedTime >= movementSkillSlot1.skillCooldown)
            {
                // Check if the skill input is pressed and the skill is not already activated
                if (GameInput.inputInstance.SkillInput1() && !skill1Activated)
                {
                    // Activate the skill
                    skill1Activated = true;
                    selectedImageOutline.enabled = true;
                    // Store the activation time
                    skill1LastActivatedTime = Time.time;

                    if (movementSkillSlot1 != null)
                    {
                        // Apply skill effects
                        movementStats.moveSpeed *= movementSkillSlot1.walkSpeedMultiplier;

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
        yield return new WaitForSeconds(movementSkillSlot1.skillDuration);

        // Deactivate the skill
        selectedImageOutline.enabled = false;
        DeactivateSkill1();

        // Start the cooldown timer and slider update after the skill duration has ended
        StartCoroutine(StartCooldownAndSliderUpdate());
    }

    private IEnumerator StartCooldownAndSliderUpdate()
    {
        isCooldownActive = true;

        // Wait for the remaining cooldown time
        yield return new WaitForSeconds(movementSkillSlot1.skillCooldown);

        // Reset the activation status and cooldown flag after cooldown
        skill1Activated = false;
        isCooldownActive = false;

        
        
    }

    private void DeactivateSkill1()
    {
        if (movementSkillSlot1 != null)
        {
            // Revert the changes made by the skill
            movementStats.moveSpeed /= movementSkillSlot1.walkSpeedMultiplier;
        }
    }

    private void UpdateCooldownSlider()
    {
        if (isCooldownActive)
        {

            float elapsedTime = Time.time - (skill1LastActivatedTime + movementSkillSlot1.skillDuration);
            Debug.Log(elapsedTime);
            float cooldownProgress = Mathf.Clamp01(elapsedTime / (movementSkillSlot1.skillCooldown));
            skill1Slider.value = cooldownProgress; // Adjusted for correct progress representation
        }
        else
        {
            // Skill not activated, reset slider
            skill1Slider.value = 0f;
        }
    }





    private MovementSkill CheckSkillIsMovementType(Skill selectedSkill)
    {
        if (selectedSkill1 is MovementSkill)
        {
            MovementSkill skill = (MovementSkill)selectedSkill1;
            return skill;
        }
        return null;
    }
}
