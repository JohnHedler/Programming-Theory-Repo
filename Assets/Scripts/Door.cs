using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    //abstract properties; overridden in derived classes.
    protected abstract string nameOfDoor { get; set; }
    protected abstract string materialType { get; set; }
    protected abstract bool locked { get; set; }
    public abstract bool isOpen { get; set; }

    //abstract methods; overridden in derived classes.
    public abstract void Open();
    public abstract void Close();
    public abstract void Lock();
    public abstract void Unlock();
}
