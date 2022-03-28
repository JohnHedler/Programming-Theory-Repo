using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==========================
//      INHERITANCE
//==========================

public class Bunny : Animal
{
    //variables
    private string bunnyName;

    private int moveDelayTime = 1;
    private int eatDelayTime = 10;
    private float speed = 100.0f;
    private float turnSpeed = 10.0f;
    private float jumpForce = 5.0f;
    private bool moved = false;
    private bool eaten = false;

    Rigidbody bunnyRb;

    //==========================
    //      POLYMORPHISM
    //==========================

    //properties
    protected override string nameOfAnimal
    {
        //==========================
        //      ENCAPSULATION
        //==========================

        get { return bunnyName; }
        set
        {
            if (value != null && value != "")
            {
                if (value.Length > 0 && value.Length < 15)
                {
                    bunnyName = value;
                }
                else
                {
                    Debug.Log("Name cannot exceed 15 characters.");
                }
            }
            else
            {
                Debug.Log("Name cannot be null or empty.");
            }
        }
    }
    protected override string color { get; set; }
    protected override string preferredFood { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        nameOfAnimal = "Bunny";
        color = "Gray";
        preferredFood = "Carrot";

        name = bunnyName;
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

        //destroys object if it falls below map
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    //==========================
    //      ABSTRACTION
    //==========================

    //overridden move function; randomly moves forward or turns
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

    //delay between move times
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(moveDelayTime);
        moved = false;
    }

    //overridden eat function; writes to debug that it ate (could do more than that)
    protected override void Eat()
    {
        eaten = true;
        Debug.Log($"{nameOfAnimal} eats a {preferredFood}.");
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
        return $"This is a {nameOfAnimal}. It is {color} in color. It likes {preferredFood}.\n";
    }
}
