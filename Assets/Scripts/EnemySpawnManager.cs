using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemy()
    {
        int random = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[random], transform.position, enemyList[random].transform.rotation);
    }
}
