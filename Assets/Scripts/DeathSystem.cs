using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    private GameObject entity;

    private void Start()
    {
        entity = gameObject;
    }

    public void Defeated()
    {
        if(entity.name == "Player")
        {
            Debug.Log("You have lost");
            //call game over from game manager
        }
        else
        {
            Destroy(entity);
        }
    }
}
