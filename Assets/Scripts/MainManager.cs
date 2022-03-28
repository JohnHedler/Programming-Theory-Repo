using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //MainManager variable
    public static MainManager instance;

    //player variables
    //public string playerName;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
}
