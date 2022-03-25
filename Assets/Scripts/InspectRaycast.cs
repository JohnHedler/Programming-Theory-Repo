using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectRaycast : MonoBehaviour
{
    private int rayLength = 5;
    private LayerMask layerMaskInteract;
    private ObjectController raycastedObj;

    private Image crosshair;
    private bool isCrosshairActive;
    private bool doOnce;

    public bool inspectOn = false;

    private void Start()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        layerMaskInteract = LayerMask.GetMask("Interact");
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (inspectOn && Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractObject"))
            {
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<ObjectController>();
                    raycastedObj.ShowObjectName();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                if (Input.GetMouseButtonDown(0))
                {
                    raycastedObj.ShowExtraInfo();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                raycastedObj.HideObjectName();
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}
