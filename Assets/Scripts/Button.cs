using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //variables for inspect function and button function
    private InspectController inspectController;

    public GameObject doorObj;
    private MetalDoor door;

    private void Start()
    {
        //find the MetalDoor script associated with the public doorObj object.
        door = doorObj.GetComponent<MetalDoor>();
        inspectController = GameObject.Find("Inspect Controller").GetComponent<InspectController>();
    }

    //Use function for button
    public void Use()
    {
        if (!door.isOpen)
        {
            door.Open();
        }
        else
        {
            door.Close();
        }
    }

    //ShowUseUI function; displays name of the object to user on HUD
    public void ShowUseUI()
    {
        inspectController.ShowName(name);
    }

    //HideUseUI function; hides the name of the object from user on HUD
    public void HideUseUI()
    {
        inspectController.HideName();
    }
}
