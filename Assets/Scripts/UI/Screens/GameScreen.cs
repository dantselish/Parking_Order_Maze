using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text levelIndexText;
    [SerializeField] private TMP_Text movesLeftText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button nextLevelPopupBtn;
    [SerializeField] private Button restartLevelPopupBtn;
    [SerializeField] private WinPopup winPopup;
    [SerializeField] private LosePopup losePopup;


    private void Awake()
    {
        homeBtn.onClick.AddListener(OnHomeClicked);
        restartBtn.onClick.AddListener(OnRestartClicked);
        nextLevelPopupBtn.onClick.AddListener(OnNextLevelClicked);
        restartLevelPopupBtn.onClick.AddListener(OnRestartClicked);

        winPopup.gameObject.SetActive(false);
        losePopup.gameObject.SetActive(false);
    }

    private void Start()
    {
        LevelsManager.Instance.MovesLeftChanged += OnMovesLeftChanged;
        LevelsManager.Instance.LevelFinished += OnLevelFinished;
        GameManager.Instance.LevelChanged += OnLevelChanged;

        SetLevelIndex(GameManager.Instance.levelIndex);
    }

    private void Update()
    {
        SetTimerText(LevelsManager.Instance.TimeRemaining);
    }

    private void SetLevelIndex(int index)
    {
        levelIndexText.SetText((++index).ToString());
    }

    private void SetMovesLeft(int movesLeft)
    {
    }

    private void SetTimerText(float timeLeft)
    {
        timerText.SetText($"{(int) timeLeft / 60:00}:{timeLeft % 60:00}");
    }

    private void OnNextLevelClicked()
    {
        LevelsManager.Instance.StartNextLevel();
    }

    private void OnHomeClicked()
    {
        GameManager.Instance.SceneLoader.LoadMenuScene();
    }

    private void OnRestartClicked()
    {
        LevelsManager.Instance.RestartLevel();
        losePopup.gameObject.SetActive(false);
        winPopup.gameObject.SetActive(false);
    }

    private void OnMovesLeftChanged(int movesLeft)
    {
        SetMovesLeft(movesLeft);
    }

    private void OnLevelChanged(int levelIndex)
    {
        SetLevelIndex(levelIndex);
        SetMovesLeft(LevelsManager.Instance.MovesLeft);
        if (winPopup)
        {
            winPopup.gameObject.SetActive(false);
        }

        if (losePopup)
        {
            losePopup.gameObject.SetActive(false);
        }
    }

    private void OnLevelFinished(bool isWin)
    {
        if (isWin)
        {
            winPopup.SetTimeAndReward(LevelsManager.Instance.TimeRemaining, LevelsManager.Instance.RewardForLevel);
            winPopup.gameObject.SetActive(true);
        }
        else
        {
            losePopup.gameObject.SetActive(true);
        }
    }
}
