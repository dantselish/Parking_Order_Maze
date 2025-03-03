using System;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float collisionForcePower;
    [SerializeField] private ParticleSystem crashParticlePrefab;
    [SerializeField] private ParticleSystem exhaustParticle;
    [SerializeField] private List<TrailRenderer> trails;

    public event Action onClicked;
    public event Action onCollision;

    private bool canBeActivated = false;


    private void Awake()
    {
        SetTrailsActive();
        ClearTrailAndStopEmitting();
        SmokeStopEmitting();

        onClicked += OnCarStarted;
    }

    public void SetCanBeActivated()
    {
        canBeActivated = true;
    }

    public void Restart()
    {
        SetCanBeActivated();
        ClearTrailAndStopEmitting();
        SmokeStopEmitting();
        SoundPlayer.Instance.StopCarEngine();
    }

    public void ReachDestination()
    {
        SmokeStopEmitting();
        SoundPlayer.Instance.StopCarEngine();
    }

    private void ApplyCollisionForce(Vector3 forcePoint, Vector3 forceDir)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        forceDir.y = 1f;
        rigidbody.AddForceAtPosition(forceDir * collisionForcePower, forcePoint, ForceMode.Impulse);
        
        onCollision?.Invoke();
    }

    private void SmokeStartEmitting()
    {
        exhaustParticle.Play();
    }

    private void SmokeStopEmitting()
    {
        exhaustParticle.Stop();
    }

    private void SetTrailsActive()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.gameObject.SetActive(true);
        }
    }

    private void TrailStopEmitting()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = false;
        }
    }

    private void ClearTrailAndStopEmitting()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.Clear();
            trail.emitting = false;
        }
    }

    private void TrailStartEmitting()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = true;
        }
    }

    private void OnCarStarted()
    {
        canBeActivated = false;
        TrailStartEmitting();
        SmokeStartEmitting();
        SoundPlayer.Instance.PlayCarStart();
        SoundPlayer.Instance.PlayCarEngine();
        GameManager.Instance.AchievementsManager.AddMoveMade();
    }

    private void OnCarCrash(ContactPoint contact)
    {
        ApplyCollisionForce(contact.point, contact.normal);
        Instantiate(crashParticlePrefab, contact.point, Quaternion.identity);
        TrailStopEmitting();
        SmokeStopEmitting();
        SoundPlayer.Instance.PlayCarCrash();
        SoundPlayer.Instance.StopCarEngine();
        GameManager.Instance.AchievementsManager.AddCarsCrashed();
    }

    private void OnMouseDown()
    {
        if (!canBeActivated)
        {
            return;
        }

        onClicked?.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        Car otherCar = other.gameObject.GetComponent<Car>();
        if (!otherCar)
        {
            return;
        }

        ContactPoint contact = other.GetContact(0);
        OnCarCrash(contact);
    }
}
