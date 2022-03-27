/***********************************************************************
 * Experimental Drone Script (Uses NavMeshAgent)
 * -Add NavMeshAgent component to object
 * -Add DroneExperimental script to object
 * -Download and import NavMeshSurface assets
 *      from https://github.com/Unity-Technologies/NavMeshComponents
 * -NavMeshSurface documentation
 *      at https://docs.unity3d.com/Manual/class-NavMeshSurface.html
 * -Youtube video about NavMeshAgent and NavMeshSurface
 *      at https://www.youtube.com/watch?v=UjkSFoLxesw&t=182s
 * -Create new object and add NavMeshSurface script to it
 * -Add layers to 'Include Layers' and click [Bake]
 * -In Unity, add objects and variables to enemy object
 *      such as layermasks (whatIsGround, whatIsPlayer)
 ***********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneExperimental : MonoBehaviour
{
    //AI walk/hunt variables
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling variables
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    //Attacking variables
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States of enemy object
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //health
    private int health = 3;

    //projectile
    private GameObject enemyProjectile;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for player in sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        //Calcualte random walk point
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        //Make sure enemy does not move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
}
