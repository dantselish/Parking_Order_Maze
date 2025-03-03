using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VolumeButton : MonoBehaviour
{
    [SerializeField] private Image  image;
    [SerializeField] private Sprite mutedStateSprite;
    [SerializeField] private Sprite unmutedStateSprite;

    public Button Button { get; private set; }


    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    public void SetSprite(bool isMuted)
    {
        image.sprite = isMuted ? mutedStateSprite : unmutedStateSprite;
    }
}
