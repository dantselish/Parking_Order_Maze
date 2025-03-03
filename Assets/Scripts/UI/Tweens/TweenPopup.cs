using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TweenPopup : TweenUI
{
    [SerializeField] private float delay;
    [SerializeField] private float duration;

    [SerializeField] private Ease ease;

    private RectTransform rectTransform;

    private Vector3 defaultScale = Vector3.negativeInfinity;


    private void OnEnable()
    {
        Tween();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public override void Tween()
    {
        rectTransform ??= GetComponent<RectTransform>();
        if (defaultScale.Equals(Vector3.negativeInfinity))
        {
            defaultScale = rectTransform.localScale;
        }

        rectTransform.localScale = Vector3.zero;
        StartCoroutine(scaleDelay());

        IEnumerator scaleDelay()
        {
            yield return new WaitForSeconds(delay);
            rectTransform.DOScale(defaultScale, duration).SetEase(ease);
        }
    }
}
