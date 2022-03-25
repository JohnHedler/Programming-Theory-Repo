using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDoor : Door
{
    //properties
    protected override string nameOfDoor { get; set; }
    protected override string materialType { get; set; }
    protected override bool locked { get; set; }
    public override bool isOpen { get; set; }

    private void Start()
    {
        nameOfDoor = "Metal Door";
        materialType = "Metal";
        locked = false;
        name = nameOfDoor;
    }

    //methods
    public override void Open()
    {
        if(!locked && !isOpen)
        {
            isOpen = true;
            transform.Translate(Vector3.left * 10);
        }
    }

    public override void Close()
    {
        if (!locked && isOpen)
        {
            transform.Translate(Vector3.right * 10);
            isOpen = false;
        }
    }

    public override void Lock()
    {
        locked = true;
    }

    public override void Unlock()
    {
        locked = false;
    }
}
