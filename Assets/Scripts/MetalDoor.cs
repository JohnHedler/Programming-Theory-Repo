using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDoor : Door
{
    //overridden properties from Door superclass
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

    //Overridden Open function; checks if door is unlocked and closed, and if so, opens.
    public override void Open()
    {
        if(!locked && !isOpen)
        {
            isOpen = true;
            transform.Translate(Vector3.left * 10);
        }
    }

    //Overridden close function; checks if door is unlocked and open, and if so, closes.
    public override void Close()
    {
        if (!locked && isOpen)
        {
            transform.Translate(Vector3.right * 10);
            isOpen = false;
        }
    }

    //Overridden lock function; locks the door to prevent 'Open' and 'Close' functions to execute.
    public override void Lock()
    {
        locked = true;
    }

    //Overridden unlock function; unlocks the door to allow 'Open' and 'Close' functions to execute.
    public override void Unlock()
    {
        locked = false;
    }
}
