using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private InspectController inspectController;

    public GameObject doorObj;
    private MetalDoor door;

    private void Start()
    {
        door = doorObj.GetComponent<MetalDoor>();
        inspectController = GameObject.Find("Inspect Controller").GetComponent<InspectController>();
    }

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

    public void ShowUseUI()
    {
        inspectController.ShowName(name);
    }

    public void HideUseUI()
    {
        inspectController.HideName();
    }
}
