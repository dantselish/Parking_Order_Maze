using UnityEngine;

public class PlayerPrefsManager
{
    private const string LevelIndexKey = "LevelIndex";
    private const string MoneyKey = "Money";
    private const string MovesMadeKey = "MovesMade";
    private const string CarsCrashedKey = "CarsCrashed";


    public void SaveLevelIndex(int levelIndex)
    {
        PlayerPrefs.SetInt(LevelIndexKey, levelIndex);
    }

    public void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt(MoneyKey, amount);
    }

    public void SaveMovesMade(int count)
    {
        PlayerPrefs.SetInt(MovesMadeKey, count);
    }

    public void SaveCarsCrashed(int count)
    {
        PlayerPrefs.SetInt(CarsCrashedKey, count);
    }

    public int ReadCarsCrashed()
    {
        return PlayerPrefs.GetInt(CarsCrashedKey);
    }

    public int ReadMovesMade()
    {
        return PlayerPrefs.GetInt(MovesMadeKey);
    }

    public int ReadLevelIndex()
    {
        return PlayerPrefs.GetInt(LevelIndexKey);
    }

    public int ReadMoney()
    {
        return PlayerPrefs.GetInt(MoneyKey);
    }
}
