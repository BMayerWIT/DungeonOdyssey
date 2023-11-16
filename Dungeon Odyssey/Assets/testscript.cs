using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    private Vector3 yVelocity;
    private float gravity = -9.8f;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (!characterController.isGrounded) 
        {
            yVelocity.y += gravity * Time.deltaTime;
            characterController.Move(yVelocity * Time.deltaTime);
        }
    }
}
