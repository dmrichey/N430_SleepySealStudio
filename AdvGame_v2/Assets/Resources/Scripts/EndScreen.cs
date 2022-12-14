using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    float elapsedTime;
    int numCollected;

    public TextMeshProUGUI textBoxTime;
    public TextMeshProUGUI textBoxCollected;

    void Awake()
    {
        var pauseMenu = GameObject.Find("PauseMenu");
        elapsedTime = pauseMenu.GetComponentInParent<PauseMenu>().elapsedTime;
        numCollected = pauseMenu.GetComponentInParent<PauseMenu>().numCollected;

        var ts = TimeSpan.FromSeconds(elapsedTime);
        textBoxTime.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        textBoxCollected.text = numCollected + " out of 14";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
