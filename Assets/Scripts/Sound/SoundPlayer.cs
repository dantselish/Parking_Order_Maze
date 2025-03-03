using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }

    [SerializeField] private AudioSource carCrash;
    [SerializeField] private AudioSource carEngine;
    [SerializeField] private AudioSource carStart;
    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource lose;

    private int carEngineCounter;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlayCarCrash()
    {
        if (!carCrash.isPlaying)
        {
            carCrash.Play();
        }
    }

    public void PlayCarStart()
    {
        carStart.Play();
    }

    public void PlayButtonPress()
    {
        buttonPress.Play();
    }

    public void PlayCarEngine()
    {
        ++carEngineCounter;
        carEngine.Play();
    }

    public void StopCarEngine()
    {
        --carEngineCounter;
        if (carEngineCounter <= 0)
        {
            carEngineCounter = 0;
            carEngine.Stop();
        }
    }

    public void PlayLevelFinished(bool isWin)
    {
        if (isWin)
        {
            win.Play();
        }
        else
        {
            lose.Play();
        }
    }
}
