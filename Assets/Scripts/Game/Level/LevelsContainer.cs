using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsContainer", menuName = "ScriptableObjects/LevelContainer")]
public class LevelsContainer : ScriptableObject
{
    [SerializeField] private List<Level> levels;


    public List<Level> GetAllLevels()
    {
        return levels;
    }

    public Level GetLevel(int index)
    {
        if (index >= levels.Count)
        {
            index %= levels.Count;
        }

        return levels[index];
    }
}
