using System;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }

    [SerializeField] private LevelsContainer levelsContainer;
    [SerializeField] private Tutorial tutorialPrefab;

    private Level currentLevel;

    public event Action<int>  MovesLeftChanged;
    public event Action<bool> LevelFinished;

    public int CurrentLevelIndex { get; private set; }

    public int MovesLeft => currentLevel.MovesRemaining;

    public float TimeRemaining => currentLevel ? currentLevel.TimeRemaining : 60;
    public int RewardForLevel => currentLevel.Reward;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartLevel(GameManager.Instance.levelIndex);
    }

    public void RestartLevel()
    {
        currentLevel.RestartLevel();
        MovesLeftChanged?.Invoke(currentLevel.MovesRemaining);
    }

    public void StartNextLevel()
    {
        StartLevel(++CurrentLevelIndex);
    }

    private void StartLevel(int index)
    {
        Level newLevelPrefab = levelsContainer.GetLevel(index);
        if (currentLevel)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(newLevelPrefab, transform);

        currentLevel.levelFinished += CurrentLevelOnLevelFinished;
        currentLevel.moveMade += CurrentLevelOnMoveMade;

        GameManager.Instance.ChangeLevel(index);

        if (index == 0)
        {
            Tutorial tutorial = Instantiate(tutorialPrefab);
            tutorial.TutorialFinished += TutorialOnTutorialFinished;
        }
        else
        {
            currentLevel.StartLevel();
        }
    }

    private void CurrentLevelOnMoveMade()
    {
        MovesLeftChanged?.Invoke(MovesLeft);
    }

    private void CurrentLevelOnLevelFinished(bool isWin)
    {
        LevelFinished?.Invoke(isWin);
        SoundPlayer.Instance.PlayLevelFinished(isWin);
        if (isWin)
        {
            GameManager.Instance.AddMoneyForLevel(currentLevel);
        }
    }

    private void TutorialOnTutorialFinished()
    {
        currentLevel.StartLevel();
    }
}
