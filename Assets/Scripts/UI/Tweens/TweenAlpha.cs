using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TweenAlpha : TweenUI
{
    [SerializeField] private float time;

    private void OnEnable()
    {
        Tween();
    }

    public override void Tween()
    {
        Image image = GetComponent<Image>();

        float startAlpha = image.color.a;

        Color color = image.color;
        color.a = 0f;
        image.color = color;

        image.DOFade(startAlpha, time);
    }
}
