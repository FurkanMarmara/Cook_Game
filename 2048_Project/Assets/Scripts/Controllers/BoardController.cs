using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardController : MonoBehaviour
{
    #region Singleton
    private static BoardController _instance;

    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }
    #endregion

    public static BoardController Instance()
    {
        return _instance;
    }

    //TODO: CHANGE PNG TO FOODS

    [SerializeField]
    private Food[,] _board;

    [SerializeField]
    private List<Food> _firstFoods = new List<Food>();

    [SerializeField]
    private int _selectedMap = 0;

    [SerializeField]
    private ScoreController _scoreController;

    private int _gridCount = 4;

    private Vector2[,] _gridPositions;

    private int _emptyCount;


    private void Start()
    {
        _emptyCount = _gridCount * _gridCount;
        _board = new Food[_gridCount, _gridCount];
        _gridPositions = new Vector2[_gridCount, _gridCount];
        SetGridPositions();
    }

    //TODO: Change Constants from gridCount
    private void SetGridPositions() 
    {
        float posX = Constants.START_POS_X_4x4;
        float posY = Constants.START_POS_Y_4x4;
        for (int i = 0; i < _gridCount; i++)
        {
            for (int j = 0; j < _gridCount; j++)
            {
                _gridPositions[i, j] = new Vector2(posX, posY);
                posX += Constants.INCREASE_4X4;
            }
            posX = Constants.START_POS_X_4x4;
            posY -= Constants.INCREASE_4X4;
        }
    }

    private IEnumerator SpawnFood(int count,float delayTime)
    {
        WaitForSeconds waitTime = new WaitForSeconds(delayTime);
        yield return waitTime;
        int randomX = 0;
        int randomY = 0;
        if (_emptyCount>0)
        {
            for (int i = 0; i < count; i++)
            {
                randomX = Random.Range(0, _gridCount);
                randomY = Random.Range(0, _gridCount);
                if (_board[randomX, randomY] == null)
                {
                    GameObject gO = Instantiate(_firstFoods[_selectedMap].gameObject, _gridPositions[randomX, randomY], Quaternion.identity);
                    Food food = _board[randomX, randomY] = gO.GetComponent<Food>();
                    food.SetGridValue(randomX, randomY);
                    _emptyCount -= 1;
                }
                else
                {
                    //Debug.Log("Test Count");
                    i--;
                }
            }
            if (_emptyCount==0)
            {
                CanMoveControl();
            }
        }
    }

    private void CanMoveControl()
    {
        int canMoveCount = 0;
        for (int i = 0; i < _gridCount; i++)
        {
            for (int j = 0; j < _gridCount; j++)
            {
               canMoveCount += _board[i, j].CanMove();
            }
        }
        if (canMoveCount==0)
        {
            //Debug.Log("Game Over");
            GameManager.Instance().GameOver();
        }
        //Debug.Log("Move Count = " + canMoveCount);
    }

    private void ResetToGame(bool tryAgain)
    {
        for (int i = 0; i < _gridCount; i++)
        {
            for (int j = 0; j < _gridCount; j++)
            {
                if (_board[i,j]!=null)
                {
                    Destroy(_board[i, j].gameObject);
                }
            }
        }
        _emptyCount = _gridCount * _gridCount;
        if (tryAgain)
        {
            StartCoroutine(SpawnFood(2,0));
        }
    }

    public void SetMap(int mapNumber)
    {
        _selectedMap = mapNumber;
    }

    public void ChangeGridsFromDirection(Directions direction)//TODO: FOR GRİD COUNT'A GORE DUZENLENECEK
    {
        int changedGridCount = 0;
        switch (direction)
        {
            case Directions.Up:
                for (int i = 1; i < _gridCount; i++)
                {
                    for (int j = 0; j < _gridCount; j++)
                    {
                        if (_board[i, j] != null)//Is Cell Empty ?
                        {
                            changedGridCount += _board[i, j].ChangeGridByDirection(direction);
                        }
                    }
                }
                break;
            case Directions.Right:
                int counterY = 2;
                for (int i = 0; i < _gridCount - 1; i++)
                {
                    int counterX = 0;
                    for (int j = 0; j < _gridCount; j++)
                    {
                        if (_board[counterX, counterY] != null)//Is Cell Empty ?
                        {
                            changedGridCount += _board[counterX, counterY].ChangeGridByDirection(direction);
                        }
                        counterX++;
                    }
                    counterY--;
                }
                break;
            case Directions.Down:
                for (int i = 2; i >= 0; i--)
                {
                    for (int j = 0; j < _gridCount; j++)
                    {
                        if (_board[i, j] != null)//Is Cell Empty ?
                        {
                            changedGridCount += _board[i, j].ChangeGridByDirection(direction);
                        }

                    }
                }
                break;
            case Directions.Left:
                int counterY2 = 1;
                for (int i = 0; i < _gridCount - 1; i++)
                {
                    int counterX = 0;
                    for (int j = 0; j < _gridCount; j++)
                    {
                        if (_board[counterX, counterY2] != null)//Is Cell Empty ?
                        {
                            changedGridCount += _board[counterX, counterY2].ChangeGridByDirection(direction);
                        }
                        counterX++;
                    }
                    counterY2++;
                }

                break;
            default:
                break;
        }
        if (changedGridCount>0)
        {
            StartCoroutine(SpawnFood(1,0.2f));
        }
        
    }

    public Food GetFoodByGridValues(int x,int y)
    {
        if (_board[x,y]==null)
        {
            return null;
        }
        else
        {
            return _board[x, y];
        }
    }

    public void ChangeGridValue(int x,int y,Food food)
    {
        _board[x, y] = food;
    }

    public Vector2 GetVectorByGridValues(int x,int y)
    {
        return _gridPositions[x, y];
    }
    
    public void IncreaseEmptyCount()
    {
        _emptyCount++;
    }

    public int GetGridCount()
    {
        return _gridCount;
    }
   
    public void StartToGame()
    {
        StartCoroutine(SpawnFood(2,0));
    }

    public void TryAgain(bool tryAgain)
    {
        ResetToGame(tryAgain);
        _scoreController.ResetScore();
    }

    public void LevelUp()
    {
        _selectedMap++;
        ResetToGame(true);
        _scoreController.ResetScore();
    }

    public void SetScore(int score)
    {
        _scoreController.SetScore(score);
    }//Bu GameManagerda olmalı.

    public int GetScore()//Bu GameManagerda olmalı.
    {
        return _scoreController.GetScore();
    }
   
    
}


