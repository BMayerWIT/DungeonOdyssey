using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    [Header("Objects")]
    
    [SerializeField] private Camera firstPersonCamera;
    
    
    private PlayerAnimationHandler playerAnimationHandler;

    
    private float standingHeight;
    private bool isPlayerGrounded;
    
   
    

    [Header("Interacting")]
    [SerializeField] private float interactDistance;
    


    private void Start()
    {
        
       // standingHeight = characterController.height;
        

        
        
        playerAnimationHandler= GetComponent<PlayerAnimationHandler>();
        
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        GameInput.inputInstance.TickInput(delta);

        HandleDash();
        
        

        
    }

    

   

    public void HandleDash()
    {
        //if (playerAnimator.GetBool("isInteracting"))
        //{ return; }


        if (!GameInput.inputInstance.dashFlag && GameInput.inputInstance.HandleDashInput())
        {
            Debug.Log("Animation should play");
            
            //playerAnimationHandler.PlayTargetAnimation("DashBack", true);
        }
        
    }

  

    public RaycastHit InteractableCheck()
    {
        Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out RaycastHit hit, interactDistance);
        Debug.DrawRay(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, Color.red);
        return hit;
    }

    
    
}
