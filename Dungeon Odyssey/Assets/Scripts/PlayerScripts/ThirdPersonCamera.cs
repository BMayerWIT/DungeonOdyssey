using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    private float FPsensitivity;
    

    

 

    [SerializeField] private Camera fPCamera;
    
    

    private float mouseX;
    private float mouseY;
    

    float xRotation;
    float yRotation;
    


    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        fPCamera.enabled = true;
        

    }

    private void Update()
    {
        mouseX = GameInput.inputInstance.GetMouseX();
        mouseY = GameInput.inputInstance.GetMouseY();
        
        
        
            FPsensitivity = PlayerPrefs.GetFloat("firstPersonSensitivity");
            yRotation += mouseX * FPsensitivity;
            xRotation -= mouseY * FPsensitivity;
        
        
       

        
    }

    private void FixedUpdate()
    {
      
        HandleFPCamera();
    }

  
 
    private void HandleFPCamera()
    {
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        
        fPCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation , 0);
    }

   

}
