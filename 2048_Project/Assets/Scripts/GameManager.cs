using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField]
    private BoardController _boardController;
    [SerializeField]
    private UIController _uIController;

    public static GameManager Instance()
    {
        return _instance;
    }

    public void StartToGame()
    {
        _boardController.StartToGame();
    }

    public void TryAgain(bool tryAgain)
    {
        _boardController.TryAgain(tryAgain);
    }

    public void GameOver()
    {
        _uIController.GameOver();
    }

}
