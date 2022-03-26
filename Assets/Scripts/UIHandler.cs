using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private GameObject tutorialPanel;

    private Image heart1;
    private Image heart2;
    private Image heart3;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        tutorialPanel = GameObject.Find("Tutorial Panel");

        heart1 = GameObject.Find("Heart1").GetComponent<Image>();
        heart2 = GameObject.Find("Heart2").GetComponent<Image>();
        heart3 = GameObject.Find("Heart3").GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(int health)
    {
        switch (health)
        {
            case 0:
                {
                    heart1.sprite = emptyHeart;
                    heart2.sprite = emptyHeart;
                    heart3.sprite = emptyHeart;
                    break;
                }
            case 1:
                {
                    heart1.sprite = fullHeart;
                    heart2.sprite = emptyHeart;
                    heart3.sprite = emptyHeart;
                    break;
                }
            case 2:
                {
                    heart1.sprite = fullHeart;
                    heart2.sprite = fullHeart;
                    heart3.sprite = emptyHeart;
                    break;
                }
            case 3:
                {
                    heart1.sprite = fullHeart;
                    heart2.sprite = fullHeart;
                    heart3.sprite = fullHeart;
                    break;
                }
            default:
                {
                    heart1.sprite = emptyHeart;
                    heart2.sprite = emptyHeart;
                    heart3.sprite = emptyHeart;
                    break;
                }
        }
    }

    public void ToggleTutorial()
    {
        if (tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(false);
        }
        else
        {
            tutorialPanel.SetActive(true);
        }
    }
}
