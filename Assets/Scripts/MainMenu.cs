using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        Debug.Log("SinglePlayer is loading...");
        SceneManager.LoadScene("SinglePlayer", LoadSceneMode.Single);
    }

    public void LoadCoopModeGame()
    {
        Debug.Log("Co-op Mode is loading...");
        SceneManager.LoadScene("CoopMode", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
