using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectRaycast : MonoBehaviour
{
    private int rayLength = 5;
    private LayerMask layerMaskInteract;
    private ObjectController raycastedObj;
    private InspectController inspectController;
    private Button raycastedObjButton;

    public Sprite crosshairOne;
    public Sprite crosshairTwo;
    private RectTransform crosshair;
    private Image crosshairImage;
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

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
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
