using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public Transform defaultTransform; // The default position and rotation
    public Transform targetTransform;  // The target position and rotation
    public Transform menuTransform;
    public GameObject mainCam;
    public GameObject menu;
    public float smoothSpeed = 5f;      // Adjust the speed of the movement
    public bool isBuying;
    public bool returnToMenu;

    private bool playHelloLine;
    private bool playGoodbyeLine;
    private bool playIsBuyingLine;

    void Start()
    {

        
        // Set the default position and rotation initially
        if (defaultTransform != null)
        {
            transform.position = defaultTransform.position;
            transform.rotation = defaultTransform.rotation;
        }
        else
        {
            Debug.LogWarning("Default transform is not assigned.");
        }

        StartCoroutine(PlayWelcomeLines());
    }

    void Update()
    {
        // Check if the targetTransform is assigned
        if (targetTransform == null)
        {
            Debug.LogWarning("Target transform is not assigned.");
            return;
        }

        HandleIsBuying();
        HandleMenu();
    }

    void MoveCamera(Transform target)
    {
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, target.position, smoothSpeed * Time.deltaTime);
      


    }

    void RotateCamera(Transform target)
    {

        // Use Quaternion.Lerp to smoothly interpolate between current and target rotations
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
      

    }

    public void HandleIsBuying()
    {
        // Smoothly move the camera to the new position
        if (isBuying)
        {
            if (playIsBuyingLine) 
            {
                AudioManager.Instance.PlayRandomStartBuying();
            }
            
            MoveCamera(targetTransform);
            // Smoothly rotate the camera to the new rotation
            RotateCamera(targetTransform);
            playIsBuyingLine = false;
        }
        else if (!isBuying)
        {
            MoveCamera(defaultTransform);
            // Smoothly rotate the camera to the new rotation
            RotateCamera(defaultTransform);
            playIsBuyingLine = true;
        }

    }

    public void HandleMenu() 
    { 
        if (returnToMenu)
        {
            if (playGoodbyeLine)
            {
                AudioManager.Instance.PlayRandomGoodbye();
            }
            
            MoveCamera(menuTransform);
            // Smoothly rotate the camera to the new rotation
            RotateCamera(menuTransform);
            //menu.SetActive(true);
            playGoodbyeLine = false;

        }
        else
        {

            playGoodbyeLine = true;
            MoveCamera(defaultTransform);
            // Smoothly rotate the camera to the new rotation
            RotateCamera(defaultTransform);
           // menu.SetActive(false);
        }
    }

    private IEnumerator PlayWelcomeLines()
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayRandomShopWelcome();
    }

}


