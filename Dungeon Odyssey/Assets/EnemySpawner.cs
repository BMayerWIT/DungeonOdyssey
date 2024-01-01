using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //public int spawnChance;
    //public GameObject enemyPrefab;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    int roll = Random.Range(1, 101);
    //    if (roll <= spawnChance)
    //    {
    //        GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    //    }
    //}


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 5);
        
    }



}
