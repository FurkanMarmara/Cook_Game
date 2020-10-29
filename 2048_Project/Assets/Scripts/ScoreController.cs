using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int _score = 0;



    public void SetScore(int score)
    {
        _score = score;
        GameManager.Instance().ChangeScore(_score);
    }

    public int GetScore()
    {
        return _score;
    }

    public void ResetScore()
    {
        _score = 0;
        SetScore(_score);
    }
}
