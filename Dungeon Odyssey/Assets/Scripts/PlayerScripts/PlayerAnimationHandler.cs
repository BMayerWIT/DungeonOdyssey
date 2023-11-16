using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animStateInfo;
    
    private Player player;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
       
        player = GetComponent<Player>();
    }

    private void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(1);
        MovePlayerWithAnimation();
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        //animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    private void MovePlayerWithAnimation()
    {
        
        if (animStateInfo.IsName("DashBack"))
        {

            float delta = Time.deltaTime;

            // Get the root motion delta position and rotation from the Animator
            Vector3 deltaPosition = animator.deltaPosition;
            Quaternion deltaRotation = animator.deltaRotation;

            // Remove any vertical movement (e.g., jumping or falling)
            deltaPosition.y = 0;

            // Convert the deltaRotation to a delta rotation in Euler angles
            Vector3 deltaEuler = deltaRotation.eulerAngles;

            // Apply the delta position to the character controller
            //player.characterController.Move(deltaPosition / delta);

            // Apply the delta rotation to the character's orientation
            //player.orientation.Rotate(deltaEuler);
        }
    }
}
