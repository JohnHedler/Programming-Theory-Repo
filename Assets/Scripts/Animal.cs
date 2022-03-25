using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    //properties
    protected abstract string nameOfAnimal { get; set; }
    protected abstract string color { get; set; }
    protected abstract string preferredFood { get; set; }

    //methods
    protected abstract void Move();
    protected abstract void Eat();
    public abstract string GetDescription();
}
