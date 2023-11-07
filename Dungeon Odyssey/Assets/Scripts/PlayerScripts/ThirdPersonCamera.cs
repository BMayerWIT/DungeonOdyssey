using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class ThirdPersonCamera : MonoBehaviour
{
    private Object gameInputObject;
    private GameInput gameInput;

    [SerializeField] private Transform orientation;
    private float FPsensitivity;
    private float TPsensitivity;

    

 

    [SerializeField] private Camera fPCamera;
    [SerializeField] private Camera tPCamera;
   

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
        FPsensitivity = PlayerPrefs.GetFloat("firstPersonSensitivity");
        TPsensitivity = PlayerPrefs.GetFloat("thirdPersonSensitivity");

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        gameInputObject = GameObject.Find("GameInput");
        gameInput = gameInputObject.GetComponent<GameInput>();

        fPCamera.enabled = false;
        tPCamera.enabled = true;
        offset = tPCamera.transform.position - orientation.position;

    }

    private void Update()
    {
        mouseX = gameInput.GetMouseX();
        mouseY = gameInput.GetMouseY();
        if (isFirstPerson) 
        {
            yRotation += mouseX * FPsensitivity;
            xRotation -= mouseY * FPsensitivity;
        }
        else
        {
            yRotation += mouseX * TPsensitivity;
            xRotation -= mouseY * TPsensitivity;
        }
        HandleCameraState();

        
    }

    private void FixedUpdate()
    {
        HandleTPCameraRotation();
        FollowPlayer();
        HandleFPCamera();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = orientation.position + orientation.up * offset.y - orientation.forward * distance;
        tPCamera.transform.position = Vector3.Lerp(tPCamera.transform.position, targetPosition, rotationSmoothness * Time.deltaTime);
    }

    public void HandleTPCameraRotation()
    {
        if (!isFirstPerson)
        {
            xRotation = Mathf.Clamp(xRotation, -maxYAngle, -minYAngle);
        }
        
        Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
        tPCamera.transform.rotation = Quaternion.Slerp(tPCamera.transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }

    private void HandleFPCamera()
    {
        if (isFirstPerson)
        {
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        }
        
        fPCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation , 0);
    }

    private void HandleCameraState()
    {
        if (gameInput.GetToggleCamera())
        {
            isFirstPerson = !isFirstPerson;

            fPCamera.enabled = isFirstPerson;
            tPCamera.enabled = !isFirstPerson;
        }
    }

}
