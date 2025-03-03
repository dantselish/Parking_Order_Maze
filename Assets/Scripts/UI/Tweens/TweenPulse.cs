using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TweenPulse : TweenUI
{
    [SerializeField] private float scaleFactor;
    [SerializeField] private float time;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;

    private RectTransform rectTransform;
    private Vector3 startScale = Vector3.negativeInfinity;


    private void OnEnable()
    {
        Tween();
    }

    private void OnDisable()
    {
        rectTransform.DOKill();
        if (!startScale.Equals(Vector3.negativeInfinity))
        {
            rectTransform.localScale = startScale;
        }
    }

    public override void Tween()
    {
        rectTransform ??= GetComponent<RectTransform>();

        StartCoroutine(delayRoutine());


        IEnumerator delayRoutine()
        {
            yield return new WaitForSeconds(delay);

            if (startScale.Equals(Vector3.negativeInfinity))
            {
                startScale = rectTransform.localScale;
            }
            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOScale(scaleFactor * scaleFactor, time / 2f )).SetEase(ease);
            sequence.Append(rectTransform.DOScale(startScale, time / 2f).SetEase(ease));
            sequence.SetLoops(-1);
            sequence.Play();
        }
    }
}
