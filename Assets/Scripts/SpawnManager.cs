using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAnimal()
    {
        int random = Random.Range(0, animalList.Length);
        Instantiate(animalList[random], transform.position, animalList[random].transform.rotation);
    }
}
