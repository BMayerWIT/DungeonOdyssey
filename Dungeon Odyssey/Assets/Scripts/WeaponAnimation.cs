//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WeaponAnimation : MonoBehaviour
//{
//    private Animator animator;
//    private const string IS_WALKING = "isWalking";
//    private const string IS_ATTACKING = "isAttacking";

//    [SerializeField] private Player player;
//    [SerializeField] private Weapon weapon;

//    private void Awake()
//    {
//        animator = GetComponent<Animator>();
//        animator.SetBool(IS_WALKING, player.IsWalking());
//        animator.SetBool(IS_ATTACKING, weapon.IsAttacking());
//    }

//    private void Update()
//    {
//        animator.SetBool(IS_ATTACKING, weapon.IsAttacking());

//    }

    
//}
