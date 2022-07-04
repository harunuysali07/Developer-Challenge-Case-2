using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "DifficultyData", order = 1)]
public class DifficultyData : ScriptableObject
{
    public int CurrentLevelLength
    {
        get => (int)(LevelLengthBase * Mathf.Pow(LevelLengthIncrease, DataManager.PlayerLevel));
    }

    public int LevelLengthBase;
    public float LevelLengthIncrease;
}
