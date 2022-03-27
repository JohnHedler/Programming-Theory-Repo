using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectRaycast : MonoBehaviour
{
    //Raycast variables
    private int rayLength = 5;
    private LayerMask layerMaskInteract;
    private ObjectController raycastedObj;
    private InspectController inspectController;
    private Button raycastedObjButton;

    //crosshair variables
    public Sprite crosshairOne;
    public Sprite crosshairTwo;
    private RectTransform crosshair;
    private Image crosshairImage;

    //inspect variables
    private bool isCrosshairActive;
    private bool doOnce;
    public bool inspectOn = false;

    private void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<RectTransform>();
        crosshairImage = GameObject.Find("Crosshair").GetComponent<Image>();

        layerMaskInteract = LayerMask.GetMask("Interact");

        inspectController = GameObject.Find("Inspect Controller").GetComponent<InspectController>();
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //check if Raycast hits anything in the direction the player is looking
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            //check if inspect mode is on. if so, then follow InspectObject, otherwise follow UseObject.
            if (inspectOn)
            {
                InspectObject(hit);
            }
            else
            {
                UseObject(hit);
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                if (inspectOn)
                {
                    if(raycastedObj != null)
                    {
                        raycastedObj.HideObjectName();
                    }
                }
                else
                {
                    if(raycastedObjButton != null)
                    {
                        raycastedObjButton.HideUseUI();
                    }
                }
                CrosshairChangeColor(false);
                doOnce = false;
            }
        }
    }

    //CrosshairChangeImage function; changes the sprite image and size depending on if user is in
    //  inspect mode or not.
    public void CrosshairChangeImage()
    {
        if (inspectOn)
        {
            crosshairImage.sprite = crosshairTwo;
            crosshair.sizeDelta = new Vector2(100, 100);
        }
        else
        {
            crosshairImage.sprite = crosshairOne;
            crosshair.sizeDelta = new Vector2(10, 10);
        }

        CrosshairChangeColor(false);

        inspectController.HideName();
    }

    //CrosshairChangeColor function; changes the crosshair when the user looks at something that
    // can be interacted with using Inspect mode or Use mode.
    void CrosshairChangeColor(bool on)
    {
        if (on && !doOnce)
        {
            if (inspectOn)
            {
                crosshairImage.color = Color.red;
            }
            else
            {
                crosshairImage.color = Color.red;
            }
        }
        else
        {
            if (inspectOn)
            {
                crosshairImage.color = Color.white;
                isCrosshairActive = false;
            }
            else
            {
                crosshairImage.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }

    //InspectObject function; checks if Raycast has hit an object that can be interacted with.
    //  Calls to show the user the object name, and calls for crosshair change. If the user
    //  left-clicks while looking at object, then call the object's 'ShowExtraInfo'.
    void InspectObject(RaycastHit hit)
    {
        if (hit.collider.CompareTag("InteractObject"))
        {
            if (!doOnce)
            {
                raycastedObj = hit.collider.gameObject.GetComponent<ObjectController>();
                raycastedObj.ShowObjectName();
                CrosshairChangeColor(true);
            }

            isCrosshairActive = true;
            doOnce = true;

            if (Input.GetMouseButtonDown(0))
            {
                raycastedObj.ShowExtraInfo();
            }
        }
    }

    //UseObject function; checks to see if the Raycast has hit an object that can be used.
    //  Calls to show the user the name of the object, and calls for crosshair color change.
    //  If the user presses the 'E' key, then the object's 'Use' is called.
    void UseObject(RaycastHit hit)
    {
        if (hit.collider.CompareTag("UseObject"))
        {
            if (!doOnce)
            {
                raycastedObjButton = hit.collider.gameObject.GetComponent<Button>();
                raycastedObjButton.ShowUseUI();
                CrosshairChangeColor(true);
            }

            isCrosshairActive = true;
            doOnce = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                raycastedObjButton.Use();
            }
        }
    }
}
