using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //player health system variables
    public int health = 3;

    private DeathSystem deathSystem;
    private UIHandler userInterface;

    private void Start()
    {
        userInterface = GameObject.Find("UI Handler").GetComponent<UIHandler>();
        deathSystem = GetComponent<DeathSystem>();
    }

    private void Update()
    {
        //update health HUD
        userInterface.UpdateHealth(health);

        //check player's health
        CheckHealth();
    }

    //CheckHealth function; checks if player health reaches zero or less than zero, then calls player defeated.
    public void CheckHealth()
    {
        if(health <= 0)
        {
            health = 0;

            deathSystem.Defeated();
        }
    }
}
