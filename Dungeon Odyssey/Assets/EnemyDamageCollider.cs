using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    BoxCollider damageCollider;

    public int currentWeaponDamage = 10;

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
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
        Debug.Log(collision);
        if (collision.tag == "Player")
        {
            
            print("hit player");
            
            
                StatsHandler.Instance.TakeDamage(currentWeaponDamage);
            
        }

    }
}
