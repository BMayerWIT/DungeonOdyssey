using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        StatsHandler statsHandler = other.GetComponent<StatsHandler>();

        if (statsHandler != null )
        {
            statsHandler.TakeDamage(damage);
        }
    }
}
