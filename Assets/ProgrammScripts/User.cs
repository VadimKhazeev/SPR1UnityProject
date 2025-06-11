using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string userName;

    public string StartG;
    public string firstSteps;
    public string middleSteps;
    public string lateSteps;
    //значение которое меняется с false на ture если игрок прошел тест впервые
    public string was_finished;

    public User()
    {
        userName = PlayerScores.playerName;

        StartG = PlayerScores.startGame;
        firstSteps = PlayerScores.firstStepsInGame;
        middleSteps = PlayerScores.middleStepsInGame;
        lateSteps = PlayerScores.lateStepsInGame;
        was_finished = PlayerScores.was_finishedValue;
    }
}
