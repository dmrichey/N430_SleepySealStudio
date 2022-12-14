using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public int nextScene;

    public GameObject blackout;
    public GameObject canvas;
    public float fadeTime = 1.25f;
    float fadeTimer = 0.0f;
    bool fadingIn = false;
    bool fadingOut = false;
    float fadeAmount;
    Color objectColor;

    void Awake()
    {
        fadingIn = true;
        objectColor = blackout.GetComponent<Image>().color;
    }

    void Update()
    {
        if (fadingIn || fadingOut)
        {
            if (fadeTimer < fadeTime)
            {
                fadeTimer += Time.deltaTime;
            }
        }

        if (fadingIn)
        {
            fadeAmount = 1 - fadeTimer / fadeTime;
            if (fadeTimer >= fadeTime)
            {
                fadingIn = false;
                canvas.SetActive(false);
            }
        }

        if (fadingOut)
        {
            fadeAmount = fadeTimer / fadeTime;
            if (fadeTimer >= fadeTime)
            {
                fadingOut = false;
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
        }

        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        blackout.GetComponent<Image>().color = objectColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadingOut = true;
            canvas.SetActive(true);
        }
    }
}
