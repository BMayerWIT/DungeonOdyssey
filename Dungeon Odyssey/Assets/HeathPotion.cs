using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StatsHandler.Instance.currentHealth += 25;
            AudioManager.Instance.PlayHealthPotionCollect();
            GameObject.Destroy(gameObject);
        }
    }

}
