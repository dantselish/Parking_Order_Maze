using System;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class CarPath : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private RoadMeshCreator pathMeshCreator;
    [SerializeField] private Car car;
    [SerializeField] private Transform pathEnd;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDuration;
    [SerializeField] private bool  fixedMoveTime;

    private bool moving = false;

    private float distanceTravelled;
    private float timeTravelled;

    public event Action movementStarted;
    public event Action<CarPath> movementFinished;
    public event Action carsCollided;


    private void Awake()
    {
        car.onClicked += CarOnClicked;
        car.onCollision += CarOnCollision;
        pathMeshCreator.TriggerUpdate();
    }

    private void Update()
    {
        if (moving)
        {
            if (fixedMoveTime)
            {
                timeTravelled += Time.deltaTime;
                float clampedTime = Mathf.Clamp01(timeTravelled / moveDuration);
                car.transform.position = pathCreator.path.GetPointAtTime(clampedTime, EndOfPathInstruction.Stop);
                car.transform.rotation = pathCreator.path.GetRotation(clampedTime, EndOfPathInstruction.Stop);

                if (clampedTime >= 1)
                {
                    ReachDestination();
                }
            }
            else
            {
                distanceTravelled += Time.deltaTime * moveSpeed;
                car.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                car.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

                if (distanceTravelled >= pathCreator.path.length)
                {
                    ReachDestination();
                }
            }
        }
    }

    public void SetCarToStartPosition()
    {
        car.transform.position = pathCreator.path.GetPointAtTime(0f);
        car.transform.rotation = pathCreator.path.GetRotation(0f);
    }

    public void SetPathEndPosition()
    {
        pathEnd.transform.position = pathCreator.path.GetPointAtTime(0.999f);
        pathEnd.transform.rotation = pathCreator.path.GetRotation(0.999f);
    }

    public void StartLevel()
    {
        car.SetCanBeActivated();
    }

    public void RestartLevel()
    {
        SetCarToStartPosition();
        moving = false;
        distanceTravelled = 0f;
        timeTravelled = 0f;
        car.Restart();
    }

    private void ReachDestination()
    {
        FinishMoving();
        car.ReachDestination();
        movementFinished?.Invoke(this);
    }

    private void StartMoving()
    {
        moving = true;
        movementStarted?.Invoke();
    }

    private void FinishMoving()
    {
        moving = false;
    }

    private void CarOnClicked()
    {
        StartMoving();
    }

    private void CarOnCollision()
    {
        FinishMoving();
        carsCollided?.Invoke();
    }
}
