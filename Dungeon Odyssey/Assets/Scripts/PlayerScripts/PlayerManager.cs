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
        gameInput = GameObject.Find("GameInput").GetComponent<GameInput>();
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        gameInput.isInteracting = animator.GetBool("isInteracting");
        gameInput.dashFlag = false;
        
    }

    private void LateUpdate()
    {
        gameInput.lightattack_Input = false;
        gameInput.heavyattack_Input = false;
    }
}
