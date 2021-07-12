using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreStats 
{
    private static int _previousScore, _highscore;
    private static float _mouseSens = 1.0f;

    public static int PreviousScore 
    {
        get 
        {
            return _previousScore;
        }
        set 
        {
            _previousScore = value;
        }
    }

    public static int HighScore 
    {
        get 
        {
            return _highscore;
        }
        set 
        {
            _highscore = value;
        }
    }

    public static float MouseSensitivity
    {
        get 
        {
            return _mouseSens;
        }
        set 
        {
            _mouseSens = value;
        }
    }
}
