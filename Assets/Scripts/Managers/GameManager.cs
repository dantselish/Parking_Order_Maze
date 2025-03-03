using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SceneLoader  sceneLoader;

    private PlayerPrefsManager playerPrefsManager;
    private AchievementsManager achievementsManager;

    public AudioManager AudioManager => audioManager;
    public AchievementsManager AchievementsManager => achievementsManager;
    public SceneLoader  SceneLoader  => sceneLoader;

    public event Action<int> LevelChanged;
    public event Action<int> MoneyChanged;

    public int levelIndex;

    public int moneyAmount { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Init();
        }
    }

    private void Init()
    {
        DontDestroyOnLoad(gameObject);

        playerPrefsManager = new PlayerPrefsManager();
        moneyAmount = playerPrefsManager.ReadMoney();
        levelIndex = playerPrefsManager.ReadLevelIndex();

        achievementsManager = new AchievementsManager(playerPrefsManager.ReadMovesMade(), playerPrefsManager.ReadCarsCrashed());
    }

    public void AddMoneyForLevel(Level level)
    {
        moneyAmount += level.Reward;
        MoneyChanged?.Invoke(moneyAmount);
    }

    public void ChangeLevel(int levelIndex)
    {
        this.levelIndex = levelIndex;
        LevelChanged?.Invoke(levelIndex);
    }

    #if UNITY_EDITOR
    public void SetMoney(int money)
    {
        playerPrefsManager ??= new PlayerPrefsManager();
        playerPrefsManager.SaveMoney(money);
    }

    public void SetLevel(int level)
    {
        playerPrefsManager ??= new PlayerPrefsManager();
        playerPrefsManager.SaveLevelIndex(level);
    }

    public void SetMovesMade(int count)
    {
        playerPrefsManager ??= new PlayerPrefsManager();
        playerPrefsManager.SaveMovesMade(count);
    }

    public void SetCarsCrashed(int count)
    {
        playerPrefsManager ??= new PlayerPrefsManager();
        playerPrefsManager.SaveCarsCrashed(count);
    }
    #endif

    private void OnApplicationQuit()
    {
        playerPrefsManager.SaveLevelIndex(levelIndex);
        playerPrefsManager.SaveMoney(moneyAmount);
        playerPrefsManager.SaveCarsCrashed(achievementsManager.carsCrashed);
        playerPrefsManager.SaveMovesMade(achievementsManager.movesMade);
    }
}
