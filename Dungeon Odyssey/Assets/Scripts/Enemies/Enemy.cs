using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
   
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private EnemyStats stats;
    private AnimatorStateInfo stateInfo;
    private EnemyDamageCollider damageCollider;
    private float distanceToPlayer;
    bool isWalking;
    bool followingPlayer = false;
    private int followRadius = 2;
    

    BoxCollider boxCollider;

    // FOV Settings
    private Vector3 direction;
    private bool isInFOV;
    private bool objectBetweenTarget;
    private bool checkPlayerHasBeenSeen;
    public bool hasPlayerBeenSeen;
    public float playerHiddenTimer = 2f;
    public float timeSincePlayerSeen;
    public float viewDistance = 8;
    public bool isInViewDistance;
    public float currentDistanceToPlayer;
    public bool canAttack;
    private bool canMoveToPlayer;
    public bool moveToPlayer;
    public float attackRange = 1f;
    public Animation[] attackAnimations;
    private bool idle;
    private bool gotHit;


    private void Start()
    {
        
    
    
        damageCollider = GetComponentInChildren<EnemyDamageCollider>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        

    }

    private void Update()
    {
        player = GameObject.Find("Player");
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        timeSincePlayerSeen += Time.deltaTime;
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }
        
        HitReset();
        HandleEnemyFOV();
        HandleAnimatorBools();
        HandleMovementAndCombat();
        Move();
        Attack();
        gotHit = stats.gotHit;

        //WaypointHandler();


    }

    private void HandleEnemyFOV()
    {
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        
        Debug.DrawRay(transform.position, direction * 100, Color.green);
        Debug.DrawRay(transform.forward, direction * 100, Color.blue);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(-60, Vector3.up) * transform.forward * 100, Color.magenta);
        Debug.DrawRay(transform.position, Quaternion.AngleAxis(+60, Vector3.up) * transform.forward * 100, Color.magenta);
        isInFOV = (Vector3.Dot(transform.forward.normalized, direction) > Mathf.Cos(Mathf.PI / 3));
        if (player != null)
        {
            isInViewDistance = Vector3.Distance(transform.position, player.transform.position) < viewDistance;
            currentDistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }
        
       
    }

    private void HandleMovementAndCombat()
    {
        if (Physics.Raycast(transform.position, direction * 100, out RaycastHit hit))
        {
            if (isInFOV && isInViewDistance)
            {
                if (hit.collider.gameObject.name != "Player")
                {
                    objectBetweenTarget = true;
                    if (hasPlayerBeenSeen)
                    {
                        canMoveToPlayer = true;
                        idle = false;
                        
                        
                    }
                }
                else
                {
                    objectBetweenTarget = false;
                    hasPlayerBeenSeen = true;
                    canMoveToPlayer = true;
                    idle = false;
            
                    
                }
            }
            else
            {
                idle = true;
            }

        }
    }

    private void HitReset()
    {
        if (gotHit)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        if (stateInfo.IsName("hit_1"))
        {
            print("gothit");
            stats.gotHit = false;
        }
    }

    private void Attack()
    {
        if (currentDistanceToPlayer < attackRange)
        {
            if (player != null)
            {
                transform.forward = player.transform.position;
            }
            
            canAttack = true;
            damageCollider.EnableDamageCollider();
            //int randomIndex = Random.Range(0, attackAnimations.Count());
            //attackAnimations[randomIndex].Play();
            agent.isStopped = true;
        }
        else
        {
            
            agent.isStopped= false;
            canAttack = false;
            damageCollider.DisableDamageCollider();
        }
    }

    private void Move()
    {
        if (canMoveToPlayer && !canAttack && currentDistanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            moveToPlayer = true;
            if (player != null)
            {
                agent.SetDestination(player.transform.position);
            }
            
        }
        else
        {
            moveToPlayer = false;
            agent.isStopped = true;
        }
    }

    

    private void HandleAnimatorBools()
    {
        animator.SetBool("isWalking", moveToPlayer);
        animator.SetBool("Idle", idle);
        animator.SetBool("canAttack", canAttack);
        animator.SetBool("gotHit", gotHit);
        if (gotHit)
        {
            Debug.Log(gotHit);
        }
        
    }


    private void Death()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;
        Destroy(gameObject);
        
    }

    //private void WaypointHandler()
    //{
    //    if (!followingPlayer)
    //    {
    //        isWalking = true;
    //        Vector3 nextPosition = GameObject.Find("Target" + currentTarget).transform.position;
    //        GetComponent<NavMeshAgent>().SetDestination(nextPosition);
    //        float distanceToCurrentTarget = Vector3.Distance(transform.position, nextPosition);
    //        if (distanceToCurrentTarget < 2.0) currentTarget++;
    //        if (currentTarget > 4) currentTarget = 1;
    //    }
    //}



}
