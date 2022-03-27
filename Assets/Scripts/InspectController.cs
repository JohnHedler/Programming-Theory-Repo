using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectController : MonoBehaviour
{
    //User Inspect HUD variables
    private GameObject objectNameBG;
    private Text objectNameUI;
    private GameObject extraInfoBG;
    private Text extraInfoUI;

    //Extra info timer variables
    private float onScreenTimer = 5.0f;
    private bool startTimer = false;
    private float timer = 0;

    private void Start()
    {
        objectNameBG = GameObject.Find("ItemTextBG");
        objectNameUI = GameObject.Find("ItemText").GetComponent<Text>();

        extraInfoBG = GameObject.Find("ItemDescriptionBG");
        extraInfoUI = GameObject.Find("ItemDescriptionText").GetComponent<Text>();

        objectNameBG.SetActive(false);
        extraInfoBG.SetActive(false);
    }

    private void Update()
    {
        //When startTimer is set to true, then count down to zero. Once timer reaches zero,
        //  clear the extra info off of the user's HUD on the screen.
        if (startTimer)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = 0;
                ClearAdditionalInfo();
                startTimer = false;
            }
        }
    }

    //ShowName function; displays the name background and the object name on the HUD
    public void ShowName(string objectName)
    {
        objectNameBG.SetActive(true);
        objectNameUI.text = objectName;
    }

    //HideName function; hides the name background and the object name on the HUD
    public void HideName()
    {
        objectNameBG.SetActive(false);
        objectNameUI.text = "";
    }

    //ShowAdditionalInfo function; displays the description background and text on the HUD
    public void ShowAdditionalInfo(string newInfo)
    {
        timer = onScreenTimer;
        startTimer = true;
        extraInfoBG.SetActive(true);
        extraInfoUI.text = newInfo;
    }

    //ClearAdditionalInfo function; hides the description background and text on the HUD
    void ClearAdditionalInfo()
    {
        extraInfoBG.SetActive(false);
        extraInfoUI.text = "";
    }
}
