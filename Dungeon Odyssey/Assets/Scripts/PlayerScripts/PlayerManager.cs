using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameInput gameInput;
    
    Animator animator;
    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        GameInput.inputInstance.isInteracting = animator.GetBool("isInteracting");
        GameInput.inputInstance.dashFlag = false;
        
    }

    private void LateUpdate()
    {
        GameInput.inputInstance.lightattack_Input = false;
        GameInput.inputInstance.heavyattack_Input = false;
    }
}
