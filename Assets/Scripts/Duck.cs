using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duck : Animal
{
    //variables
    private float moveDelayTime = 0.5f;
    private int eatDelayTime = 20;
    private float speed = 200.0f;
    private float turnSpeed = 15.0f;
    private bool moved = false;
    private bool eaten = false;

    Rigidbody duckRb;

    //properties
    protected override string nameOfAnimal { get; set; }
    protected override string color { get; set; }
    protected override string preferredFood { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        nameOfAnimal = "Duck";
        color = "White";
        preferredFood = "Corn";

        name = nameOfAnimal;
        duckRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moved)
        {
            Move();
        }

        if (!eaten)
        {
            Eat();
        }

        //destroy object if it falls below map.
        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    //overridden move function; randomly moves forward or turns
    protected override void Move()
    {
        moved = true;

        int random = Random.Range(1, 3);

        if (random == 1)
        {
            duckRb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
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

    //delay between move times
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(moveDelayTime);
        moved = false;
    }

    //overridden eat method; writes to debug that it ate (could do more than that)
    protected override void Eat()
    {
        eaten = true;
        Debug.Log($"{nameOfAnimal} eats some {preferredFood}.");
        StartCoroutine(EatDelay());
    }

    //delay between eat times
    IEnumerator EatDelay()
    {
        yield return new WaitForSeconds(eatDelayTime);
        eaten = false;
    }

    //overridden description method; returns name, color, and preferred food.
    public override string GetDescription()
    {
        return $"This is a {nameOfAnimal}. It is {color} in color. It likes {preferredFood}.";
    }
}
