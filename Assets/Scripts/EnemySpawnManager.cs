using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //array of enemy game objects
    public GameObject[] enemyList;

    // Start is called before the first frame update
    void Start()
    {
        //at start, spawn enemies repeatedly every 5 seconds
        InvokeRepeating("SpawnEnemy", 1, 5);
    }

    //SpawnEnemy function; picks a random enemy object from array and spawns it at the manager location.
    private void SpawnEnemy()
    {
        int random = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[random], transform.position, enemyList[random].transform.rotation);
    }
}
