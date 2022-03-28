using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Delay display of menu until game loads.
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    //StartNew function; loads the next scene after the title scene
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //RestartScene function; reloads the current scene from the gameover button.
    //  Calls MainManager to reset the game.
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Exit function; closes the application out
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
