using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button nextBtn;
    [SerializeField] private Image mascot;
    [SerializeField] private List<GameObject> stages;
    [SerializeField] private List<RectTransform> mascotPosition;

    private int currentStage;

    public event Action TutorialFinished;


    private void Awake()
    {
        gameObject.SetActive(true);
        mascot.transform.position = mascotPosition.First().transform.position;
        stages.First().SetActive(true);

        nextBtn.onClick.AddListener(NextStage);
    }

    private void NextStage()
    {
        if (currentStage >= stages.Count - 1)
        {
            TutorialFinished?.Invoke();
            Destroy(gameObject);
            return;
        }

        stages[currentStage].SetActive(false);
        stages[++currentStage].gameObject.SetActive(true);

        mascot.transform.DOMove(mascotPosition[currentStage].position, 0.5f).SetEase(Ease.OutSine);
    }
}
