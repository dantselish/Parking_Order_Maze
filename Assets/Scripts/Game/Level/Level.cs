using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<CarPath> paths;
    [SerializeField] private int totalMoves;
    [SerializeField] private int reward;
    [SerializeField] private float time;

    private List<CarPath> carsMovedSuccessfully;

    private bool isStarted;
    private bool isFinished;

    public event Action<bool> levelFinished;
    public event Action moveMade;

    public int MovesRemaining { get; private set; }

    public float TimeRemaining { get; private set; }

    public int Reward => reward;


    private void Awake()
    {
        InitValues();

        foreach (CarPath carPath in paths)
        {
            carPath.movementStarted += OnCarMovementStarted;
            carPath.movementFinished += OnCarMovementFinished;
            carPath.carsCollided += OnCarsCollided;
        }
    }

    private void Update()
    {
        if (!isFinished && isStarted)
        {
            TimeRemaining -= Time.deltaTime;

            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                FinishLevel(false);
            }
        }
    }

    public void RestartLevel()
    {
        InitValues();

        foreach (CarPath carPath in paths)
        {
            carPath.RestartLevel();
        }

        StartLevel();
    }

    public void StartLevel()
    {
        isStarted = true;

        foreach (CarPath carPath in paths)
        {
            carPath.StartLevel();
        }
    }

    private void InitValues()
    {
        MovesRemaining = totalMoves;
        TimeRemaining = time;

        isFinished = false;
        isStarted = false;

        carsMovedSuccessfully = new List<CarPath>();
    }

    private void OnCarsCollided()
    {
        FinishLevel(false);
    }

    private void FinishLevel(bool isWin)
    {
        if (isFinished)
        {
            return;
        }

        isFinished = true;
        levelFinished?.Invoke(isWin);
    }

    private void OnCarMovementFinished(CarPath carPath)
    {
        carsMovedSuccessfully.Add(carPath);

        if (carsMovedSuccessfully.Count >= paths.Count)
        {
            FinishLevel(true);
        }
    }

    private void OnCarMovementStarted()
    {
        --MovesRemaining;
        moveMade?.Invoke();
    }
}
