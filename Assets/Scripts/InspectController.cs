using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectController : MonoBehaviour
{
    private GameObject objectNameBG;
    private Text objectNameUI;

    private GameObject extraInfoBG;
    private Text extraInfoUI;

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

    public void ShowName(string objectName)
    {
        objectNameBG.SetActive(true);
        objectNameUI.text = objectName;
    }

    public void HideName()
    {
        objectNameBG.SetActive(false);
        objectNameUI.text = "";
    }

    public void ShowAdditionalInfo(string newInfo)
    {
        timer = onScreenTimer;
        startTimer = true;
        extraInfoBG.SetActive(true);
        extraInfoUI.text = newInfo;
    }

    void ClearAdditionalInfo()
    {
        extraInfoBG.SetActive(false);
        extraInfoUI.text = "";
    }
}
