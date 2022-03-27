using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    //assigned object's name
    private string objectName;

    //assigned object's description
    private string extraInfo;

    private InspectController inspectController;

    private void Start()
    {
        //get the name and description of the objects
        objectName = name;
        
        if(name == "Bunny")
        {
            Bunny bunnyScript = GetComponent<Bunny>();
            extraInfo = bunnyScript.GetDescription();
        }

        if(name == "Duck")
        {
            Duck duckScript = GetComponent<Duck>();
            extraInfo = duckScript.GetDescription();
        }

        inspectController = GameObject.Find("Inspect Controller").GetComponent<InspectController>();
    }

    //ShowObjectName function; display object name to user on HUD
    public void ShowObjectName()
    {
        inspectController.ShowName(objectName);
    }

    //HideObjectName function; hide object name from user on HUD
    public void HideObjectName()
    {
        inspectController.HideName();
    }

    //ShowExtraInfo function; show object description to user on HUD
    public void ShowExtraInfo()
    {
        inspectController.ShowAdditionalInfo(extraInfo);
    }
}
