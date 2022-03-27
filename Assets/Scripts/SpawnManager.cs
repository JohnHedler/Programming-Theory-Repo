using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //array of animal objects
    public GameObject[] animalList;

    // Start is called before the first frame update
    void Start()
    {
        //at start, spawn animal objects repeatedly every 10 seconds
        InvokeRepeating("SpawnAnimal", 1, 10);
    }

    //SpawnAnimal function; picks a random animal object from array and spawns at the spawn manager location.
    private void SpawnAnimal()
    {
        int random = Random.Range(0, animalList.Length);
        Instantiate(animalList[random], transform.position, animalList[random].transform.rotation);
    }
}
