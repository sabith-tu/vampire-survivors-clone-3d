using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerLevelingDataSO : ScriptableObject
{
    public int CurrentPlayerLevel = 1;
    public int Xp;
    public Action OnXpCollected;
    public Action OnLevelCompleted;

    [Space(10)] public LevelingData[] LevelingDatas;

    public void ResetXpAndPlayerLevel()
    {
        Xp = 0;
        CurrentPlayerLevel = 1;
    }


    public int GetXpNeededToCompleteCurrentLevel()
    {
        return LevelingDatas[CurrentPlayerLevel].XpNeededToComplet;
    }

    public int GetMaximumBasicEnemysForCurrentLevel()
    {
        return LevelingDatas[CurrentPlayerLevel].MaxBaseEnemys;
    }

    public float HowMuchPercentageXpIsCollectedForNextLevel()
    {
        return ((float)Xp / (float)LevelingDatas[CurrentPlayerLevel].XpNeededToComplet);
    }

    public void CurrentLevelCompleted()
    {
        CurrentPlayerLevel++;
        OnLevelCompleted?.Invoke();
    }

    public void AddXP()
    {
        Xp++;
        OnXpCollected?.Invoke();
        if (Xp >= GetXpNeededToCompleteCurrentLevel())
        {
            Xp = 0;
            CurrentLevelCompleted();
        }
    }
}

[System.Serializable]
public class LevelingData
{
    public int Level;
    public int XpNeededToComplet;
    public int MaxBaseEnemys;
}