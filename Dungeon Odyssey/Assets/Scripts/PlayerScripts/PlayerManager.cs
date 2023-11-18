using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;

    [Header("Player Flags")]
    public bool canDoCombo;
    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        canDoCombo = animator.GetBool("canDoCombo");
        
    }

    private void LateUpdate()
    {
        
    }
}
