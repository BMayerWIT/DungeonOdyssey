using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    public int currentWeaponDamage = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }
    
    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision);
        //if (collision.tag == "Player")
        //{
        //    StatsHandler statsHandler = collision.GetComponent<StatsHandler>();
            
        //    if (statsHandler != null )
        //    {
        //        statsHandler.TakeDamage(currentWeaponDamage);
        //    }
        //}

        if (collision.tag == "Enemy") 
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                StatsHandler.Instance.totalDamage += currentWeaponDamage;
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
