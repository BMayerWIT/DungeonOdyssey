using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator animator;
    private AnimatorStateInfo animStateInfo;
    private int comboCount = 0;
    

    private Player player;
    

    public void Start()
    {
        
        animator = GetComponent<Animator>();
       
        player = GetComponent<Player>();
    }

    private void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        HandleAnimationFlags();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        
 
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.0f);
    }

    
    public void ComboEnable()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void ComboDisable()
    {
        animator.SetBool("canDoCombo", false);
    }


    private void HandleAnimationFlags()
    {
        // Your existing code to handle animation flags
        if (animStateInfo.IsName("ArmAttackOHRight1") || animStateInfo.IsName("ArmAttackOHRight2") || animStateInfo.IsName("ArmAttackOHRight3"))
        {
            GameInput.inputInstance.isLightAttackingRH = true;
        }
        else
        {
            GameInput.inputInstance.isLightAttackingRH = false;
        }
    }
}
        
    

    

