using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    private string objectName;

    private string extraInfo;

    private InspectController inspectController;

    private void Start()
    {
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

    public void ShowObjectName()
    {
        inspectController.ShowName(objectName);
    }

    public void HideObjectName()
    {
        inspectController.HideName();
    }

    public void ShowExtraInfo()
    {
        inspectController.ShowAdditionalInfo(extraInfo);
    }
}
