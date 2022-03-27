using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : Enemy
{
    //movement variables
    private float moveDelayTime = 1f;
    private float speed = 100.0f;
    private float turnSpeed = 10.0f;
    private bool moved = false;
    Rigidbody enemyRb;

    //damage and knockback variables
    private int damage = 1;
    private float knockback = 4.0f;
    private float knockbackUp = 3.0f;

    //colliding attack cooldown variables
    private int attackCooldown = 1;
    private bool attackOnCooldown = false;

    //health and death system
    private int health = 3;
    private DeathSystem enemyDS;
    private Renderer enemyRenderer;
    private Color defaultColor;
    private Color hitColor = Color.red;
    private bool hit = false;

    //projectile
    private GameObject enemyProjectile;

    private void Start()
    {
        //find the child object associated with this specific game object
        enemyRenderer = transform.GetChild(0).GetComponent<Renderer>();

        defaultColor = enemyRenderer.material.color;
        enemyDS = GetComponent<DeathSystem>();
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!moved)
        {
            Move();
        }

        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!attackOnCooldown)
        {
            if (collision.gameObject.name == "Player")
            {
                attackOnCooldown = true;

                Rigidbody entityRb = collision.gameObject.GetComponent<Rigidbody>();

                Vector3 awayFromEnemy = collision.gameObject.transform.position - transform.position;

                entityRb.AddForce(awayFromEnemy * knockback, ForceMode.Impulse);
                entityRb.AddForce(Vector3.up * knockbackUp, ForceMode.Impulse);

                Damage();

                StartCoroutine(AttackCooldown());
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }

    public void TakeDamage(int damage)
    {
        if (!hit)
        {
            hit = true;

            enemyRenderer.material.SetColor("_Color", hitColor);

            health -= damage;

            if (health <= 0)
            {
                enemyDS.Defeated();
            }

            StartCoroutine(WasHit());
        }
    }

    IEnumerator WasHit()
    {
        yield return new WaitForSeconds(1);
        hit = false;
        enemyRenderer.material.SetColor("_Color", defaultColor);
    }

    protected override void Damage()
    {
        HealthSystem playerHealth = GameObject.Find("Player").GetComponent<HealthSystem>();
        playerHealth.health -= damage;
    }

    protected override void Move()
    {
        moved = true;

        int random = Random.Range(1, 3);

        if (random == 1)
        {
            enemyRb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }
        if (random == 2)
        {
            int random2 = Random.Range(1, 3);
            float randomTurn = Random.Range(0, 90);

            if (random2 == 1)
            {
                transform.Rotate(Vector3.up * (turnSpeed + randomTurn));
            }
            if (random2 == 2)
            {
                transform.Rotate(-Vector3.up * (turnSpeed + randomTurn));
            }
        }

        StartCoroutine(MoveDelay());
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(moveDelayTime);
        moved = false;
    }
}
