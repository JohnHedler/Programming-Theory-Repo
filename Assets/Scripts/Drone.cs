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
        //find the child object associated with this specific game object for color changing
        enemyRenderer = transform.GetChild(0).GetComponent<Renderer>();

        //get the original color of the object
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

        //destroy object if it falls below map
        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    //when colliding with something
    private void OnCollisionEnter(Collision collision)
    {
        if (!attackOnCooldown)
        {
            //check if it is the player that collides with the object
            if (collision.gameObject.name == "Player")
            {
                attackOnCooldown = true;

                Rigidbody entityRb = collision.gameObject.GetComponent<Rigidbody>();

                //check the distance between the object and the player
                Vector3 awayFromEnemy = collision.gameObject.transform.position - transform.position;

                //knock the player away from the object in the direction from the collision point
                entityRb.AddForce(awayFromEnemy * knockback, ForceMode.Impulse);
                entityRb.AddForce(Vector3.up * knockbackUp, ForceMode.Impulse);

                //damage the player
                Damage();

                //start cooldown to prevent repeated damage on multiple collisions with same object
                StartCoroutine(AttackCooldown());
            }
        }
    }

    //collision attack cooldown
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }

    //TakeDamage function; object receives damage from outside source calling function
    public void TakeDamage(int damage)
    {
        if (!hit)
        {
            hit = true;

            //change child object (body) color to red
            enemyRenderer.material.SetColor("_Color", hitColor);

            //reduce object's health points by damage taken
            health -= damage;

            //if object's health reaches zero, call function to destroy it
            if (health <= 0)
            {
                enemyDS.Defeated();
            }

            //start damage cooldown to prevent repeated collisions that do damage to object
            StartCoroutine(WasHit());
        }
    }

    //delay between taking damage; prevents additional collisions and makes object 'immune' to damage
    IEnumerator WasHit()
    {
        yield return new WaitForSeconds(1);
        hit = false;

        //return child object's (body) color back to the original color
        enemyRenderer.material.SetColor("_Color", defaultColor);
    }

    //overridden damage function; finds the player object and gets its health, then reduces it
    protected override void Damage()
    {
        HealthSystem playerHealth = GameObject.Find("Player").GetComponent<HealthSystem>();
        playerHealth.health -= damage;
    }

    //overridden move function; randomly moves forward or turns
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

    //delay between movement
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(moveDelayTime);
        moved = false;
    }
}
