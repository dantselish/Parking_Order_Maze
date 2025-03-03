using System;
using UnityEngine;
using Random=UnityEngine.Random;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private LoadingBar loadingBar;

    private float loadDuration;
    private float loadTimer;


    private void Awake()
    {
        StartLoading();
    }

    private void Update()
    {
        loadTimer += Time.deltaTime;

        if (loadTimer >= loadDuration)
        {
            GameManager.Instance.SceneLoader.LoadMenuScene();
        }
    }

    private void StartLoading()
    {
        loadDuration = Random.Range(2f, 3f);
        loadingBar.Load(loadDuration);
    }
}
