using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class StartProcess : MonoBehaviour
{
    Fade fade;

    public string StartSceneName;

    public float fadeTime;
    public float transitionTime;

    void Start()
    {
        fade = FindAnyObjectByType<Fade>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartFade()
    {
        if (fade != null)
        {
            fade.FadeIn(fadeTime);
            Invoke(nameof(StartTransition), fadeTime);
        }
    }

    void StartTransition()
    {
        if (fade != null)
        {
            Invoke(nameof(SceneTransitionToStart), transitionTime);
        }
    }
    void SceneTransitionToStart()
    {
        SceneManager.LoadScene(/*StartSceneName*/"Chiba Hiroto");
    }
}
