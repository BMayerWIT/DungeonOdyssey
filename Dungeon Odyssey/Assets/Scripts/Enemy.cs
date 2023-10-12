using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    
    private float distanceToPlayer;
    bool followPlayer;


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
        if (distanceToPlayer < 3)
        {
            followPlayer = true;
            agent.destination = player.transform.position + new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            followPlayer = false;
        }
    }

    private void HandleAnimatorBools()
    {
        animator.SetBool("followPlayer", followPlayer);
    }


    private void Death()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;
        Destroy(gameObject);
    }


}
