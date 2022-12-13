using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
