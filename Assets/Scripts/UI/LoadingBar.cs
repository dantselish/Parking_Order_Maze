using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private bool isLoading = false;


    private void Update()
    {
        if (isLoading)
        {
            slider.value += Time.deltaTime;
        }
    }

    public void Load(float seconds)
    {
        slider.value = seconds * 0.1f;
        isLoading = true;
    }
}
