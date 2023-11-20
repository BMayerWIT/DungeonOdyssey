using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [Header("WAYPOINTS")]
    [SerializeField] private int currentTarget = 1;
    public GameObject[] waypoints = new GameObject[4];

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    
    private float distanceToPlayer;
    bool isWalking;
    bool followingPlayer = false;
    private int followRadius = 2;


    BoxCollider boxCollider;

    private void Start()
    {
        
    
    
        currentHealth = maxHealth;
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");

    }

    private void Update()
    {
        
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        IsPlayerInDistance();
        HandleAnimatorBools();
        WaypointHandler();

        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    private void IsPlayerInDistance()
    {
        if (distanceToPlayer < 5)
        {
            isWalking = true;
            followingPlayer = true;
            Vector3 directionToPlayer = transform.position - player.transform.position;

            // Calculate the distance to the player
            float distanceToPlayer = directionToPlayer.magnitude;

            // If the distance to the player is less than the desired radius, adjust the destination
            if (distanceToPlayer < followRadius)
            {
                // Calculate the desired destination outside the radius
                Vector3 destination = player.transform.position + directionToPlayer.normalized * followRadius;

                // Set the NavMeshAgent's destination to the calculated point
                agent.SetDestination(destination);
            }
            else
            {
                // If already outside the radius, simply set the destination to the player's position
                agent.SetDestination(player.transform.position);
            }
            agent.speed = 2;
        }
        else
        {
            followingPlayer = false;
            
        }
    }

    private void HandleAnimatorBools()
    {
        animator.SetBool("isWalking", isWalking);
    }


    private void Death()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;
        Destroy(gameObject);
        
    }

    private void WaypointHandler()
    {
        if (!followingPlayer)
        {
            isWalking = true;
            Vector3 nextPosition = GameObject.Find("Target" + currentTarget).transform.position;
            GetComponent<NavMeshAgent>().SetDestination(nextPosition);
            float distanceToCurrentTarget = Vector3.Distance(transform.position, nextPosition);
            if (distanceToCurrentTarget < 2.0) currentTarget++;
            if (currentTarget > 4) currentTarget = 1;
        }
    }



}
