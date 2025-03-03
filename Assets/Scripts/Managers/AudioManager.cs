using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private const string SFXVolumeParameterName = "SFXVolume";

    private const float lowerVolumeBound = -80.0f;
    private const float upperVolumeBound = 0.0f;

    public bool isSFXMuted { get; private set; } = false;


    public void Init()
    {
    }

    public void ToggleSFX()
    {
        SetSFXActive(!isSFXMuted);
    }

    private void SetSFXActive(bool isMuted)
    {
        isSFXMuted = isMuted;
        audioMixer.SetFloat(SFXVolumeParameterName, isMuted ? lowerVolumeBound : upperVolumeBound);
    }
}
