using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
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
        userInterface.UpdateHealth(health);
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            deathSystem.Defeated();
        }
    }
}
