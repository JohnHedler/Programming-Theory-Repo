using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private HealthSystem playerHealth;

    protected virtual void Damage()
    {
        playerHealth.health -= 1;
    }

    protected abstract void Move();
}
