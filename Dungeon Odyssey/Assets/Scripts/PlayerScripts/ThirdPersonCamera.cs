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
    private float TPsensitivity;

    

 

    [SerializeField] private Camera fPCamera;
    //[SerializeField] private Camera tPCamera;
   

    [SerializeField] private float minYAngle = 20f;
    [SerializeField] private float maxYAngle = 70f;
    [SerializeField] private float rotationSmoothness = 10f;
    [SerializeField] private float distance = 5f;
    private Vector3 offset;

    private float mouseX;
    private float mouseY;
    private bool isFirstPerson = false;

    float xRotation;
    float yRotation;
    


    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        fPCamera.enabled = true;
        //tPCamera.enabled = false;
        //offset = tPCamera.transform.position - orientation.position;

    }

    private void Update()
    {
        mouseX = GameInput.inputInstance.GetMouseX();
        mouseY = GameInput.inputInstance.GetMouseY();
        
        
        if (isFirstPerson) 
        {
            FPsensitivity = PlayerPrefs.GetFloat("firstPersonSensitivity");
            yRotation += mouseX * FPsensitivity;
            xRotation -= mouseY * FPsensitivity;
        }
        else
        {
            TPsensitivity = PlayerPrefs.GetFloat("thirdPersonSensitivity");
            yRotation += mouseX * TPsensitivity;
            xRotation -= mouseY * TPsensitivity;
        }
        //HandleCameraState();

        
    }

    private void FixedUpdate()
    {
        //HandleTPCameraRotation();
        //FollowPlayer();
        HandleFPCamera();
    }

    //private void FollowPlayer()
    //{
    //    Vector3 targetPosition = orientation.position + orientation.up * offset.y - orientation.forward * distance;
    //    tPCamera.transform.position = Vector3.Lerp(tPCamera.transform.position, targetPosition, rotationSmoothness * Time.deltaTime);
    //}

    //public void HandleTPCameraRotation()
    //{
    //    if (!isFirstPerson)
    //    {
    //        xRotation = Mathf.Clamp(xRotation, -maxYAngle, -minYAngle);
    //    }
        
    //    Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
    //    tPCamera.transform.rotation = Quaternion.Slerp(tPCamera.transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    //}

    private void HandleFPCamera()
    {
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        
        fPCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation , 0);
    }

    //private void HandleCameraState()
    //{
    //    if (GameInput.inputInstance.GetToggleCamera())
    //    {
    //        isFirstPerson = !isFirstPerson;

    //        fPCamera.SetActive(isFirstPerson);
    //        tPCamera.enabled = !isFirstPerson;
    //    }
    //}

}
