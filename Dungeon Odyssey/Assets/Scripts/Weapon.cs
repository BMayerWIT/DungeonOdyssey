//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Weapon : MonoBehaviour
//{
//    //[SerializeField] private WeaponData weapon;
//    [SerializeField] private GameInput gameInput;
//    [SerializeField] private Camera cam;


//    //--------//
//    // ATTACK //
//    //--------//

//    private string weaponName;
//    private float attackDistance;
//    private int attackDamage;
//    private float attackSpeed;
//    private float attackDelay;
//    private Vector3 cameraOffsetPosition;
//    private LayerMask attackLayer;
//    private bool attacking = false;
//    private bool readyToAttack = true;
//    int attackCount;


//    private void Start()
//    {
        
//        //weaponName = weapon.weaponName;
//       // attackDistance = weapon.attackDistance;
//       // attackDamage = weapon.attackDamage;
//       // attackSpeed = weapon.attackSpeed;
//      //  attackDelay = weapon.attackDelay;
//      //  cameraOffsetPosition = weapon.cameraOffsetPosition;
//      //  attackLayer = weapon.attackLayer;
//    }

//    private void Update()
//    {
        
//    }

//    private void Attack()
//    {
//        if (readyToAttack || !attacking)
//        {
//            readyToAttack = false;
//            attacking = true;

//            Debug.Log("Before ResetAttack");
//            Invoke(nameof(ResetAttack), attackSpeed);
//            Debug.Log("Before AttackRaycast");
//            Invoke(nameof(AttackRaycast), attackDelay);
//        }
//    }

//    private void ResetAttack()
//    {
//        attacking = false;
//        readyToAttack = true;
//        Debug.Log("RESET");
//    }

//    private void AttackRaycast()
//    {
//        Debug.Log("ATTEMPTING RAYCAST"); 
//        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
//        {
//            Debug.Log("HIT ATTACK LAYER");
//            if (hit.transform.TryGetComponent<Enemy>(out Enemy enemy))
//            { 
//                Debug.Log("ENEMY SHOULD TAKE DAMAGE");
//                enemy.TakeDamage(attackDamage);
//            }
//        }
//    }

//    public bool IsAttacking()
//    {
//        return attacking;
//    }
//}
