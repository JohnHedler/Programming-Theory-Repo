using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private HealthSystem playerHealth;

    //virual Damage function; meant to be overridden in derived classes.
    protected virtual void Damage()
    {
        playerHealth.health -= 1;
    }

    //abstract Move function; meant to be overridden in derived classes.
    protected abstract void Move();
}
