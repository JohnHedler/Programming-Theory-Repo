using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //projectile strength
    private int projectileDamage = 1;

    //projectile speed
    private float speed = 1000.0f;
    private Rigidbody projectileRb;
    private bool fired = false;

    //projectile distance
    private float firedTime = 0;
    private float maxTime = 5;

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //add force only one time when projectile is instantiated; prevents it traveling faster
        if (!fired)
        {
            fired = true;
            projectileRb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }

        CheckFiredTime();
    }

    //CheckFiredTime method; destroys projectile object if it has been on the map too long
    private void CheckFiredTime()
    {
        firedTime += Time.deltaTime;

        if (firedTime > maxTime)
        {
            Destroy(gameObject);
        }
    }

    //When projectile object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        //if the tag is 'Enemy', then force it to take damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Drone>().TakeDamage(projectileDamage);
        }

        //destroy the projectile object when done
        Destroy(gameObject);
    }
}
