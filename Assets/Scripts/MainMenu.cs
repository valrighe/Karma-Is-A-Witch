using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Called when the X button is clicked
    public void QuitApplication()
    {
        Debug.Log("APPLICATION QUIT");
        Application.Quit();
    }
}