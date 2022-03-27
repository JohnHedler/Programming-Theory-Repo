using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    private GameObject entity;

    private void Start()
    {
        //get the game object script is assigned to
        entity = gameObject;
    }

    //Defeated function; checks if object is player or not, then performs associated task.
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
