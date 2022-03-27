using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bunny : Animal
{
    //variables
    private int moveDelayTime = 1;
    private int eatDelayTime = 10;
    private float speed = 100.0f;
    private float turnSpeed = 10.0f;
    private float jumpForce = 5.0f;
    private bool moved = false;
    private bool eaten = false;

    Rigidbody bunnyRb;

    //properties
    protected override string nameOfAnimal { get; set; }
    protected override string color { get; set; }
    protected override string preferredFood { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        nameOfAnimal = "Bunny";
        color = "Gray";
        preferredFood = "Carrot";

        name = nameOfAnimal;
        bunnyRb = GetComponent<Rigidbody>();
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

        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void Move()
    {
        moved = true;

        int random = Random.Range(1, 3);

        if (random == 1)
        {
            bunnyRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            bunnyRb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
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

    protected override void Eat()
    {
        eaten = true;
        //Debug.Log($"{nameOfAnimal} eats a {preferredFood}.");
        StartCoroutine(EatDelay());
    }

    IEnumerator EatDelay()
    {
        yield return new WaitForSeconds(eatDelayTime);
        eaten = false;
    }

    public override string GetDescription()
    {
        return $"This is a {nameOfAnimal}. It is {color} in color. It likes {preferredFood}.\n";
    }
}
