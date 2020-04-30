using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Difficulty { easy, medium, hard, nightmare};


//[Serializable]
[System.Serializable]
public class PlayerData
{
    public int points;
    public int correctCount;
    public string playerName;
    public Difficulty difficulty;

    public void ResetData()
    {
        points = 0;
        correctCount = 0;
        playerName = "";
        difficulty = Difficulty.easy;
    }
}

