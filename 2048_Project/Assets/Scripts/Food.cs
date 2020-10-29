using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    [SerializeField]
    private FoodTypes _foodType;

    [SerializeField]
    private List<Sprite> _spriteFoodTypes;

    private int _gridValueX;
    private int _gridValueY;

    private Food _myFood;

    BoardController boardControllerInstance = BoardController.Instance();

    private void Start()
    {
        _myFood = GetComponent<Food>();
    }

    private void ChangeSpriteByFoodType(int indexFoodType)
    {
        GetComponent<SpriteRenderer>().sprite = _spriteFoodTypes[indexFoodType];
    }

    private void SetPositions(Vector2 posVector)
    {
        transform.DOMove(new Vector3(posVector.x, posVector.y, 0), 0.15f);
    }

    public void ChangeFoodType(FoodTypes foodType)
    {
        _foodType = foodType;
    }

    public void SetGridValue(int x,int y)
    {
        _gridValueX = x;
        _gridValueY = y;
    }  

    public int ChangeGridByDirection(Directions direction)//TODO: Grid Count'a göre düzenlenecek
    {
        
        bool isChanged = false;
        switch (direction)
        {
            case Directions.Up:
                if (_gridValueX!=0)
                {
                    Food targetFood = boardControllerInstance.GetFoodByGridValues(_gridValueX-1, _gridValueY);
                    if (targetFood==null)
                    {
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                        boardControllerInstance.ChangeGridValue(_gridValueX - 1, _gridValueY, _myFood);
                        SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX - 1, _gridValueY));
                        SetGridValue(_gridValueX - 1, _gridValueY);
                        ChangeGridByDirection(direction);//TESTTT
                        isChanged = true;
                    }
                    else
                    {
                        //Control
                        if (_foodType==targetFood.GetFoodType())
                        {
                            DOTween.Kill(targetFood.transform);
                            Destroy(targetFood.gameObject);//Bunu Kontrol Et
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                            boardControllerInstance.ChangeGridValue(_gridValueX -1, _gridValueY, _myFood);
                            SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX - 1, _gridValueY));
                            SetGridValue(_gridValueX - 1, _gridValueY);
                            boardControllerInstance.IncreaseEmptyCount();
                            SetFoodType(_foodType);
                            isChanged = true;

                        }
                        else
                        {
                            //STOP
                        }

                    }
                }
                else
                {
                    //STOP
                }

                break;
            case Directions.Right:
                if (_gridValueY != 3)
                {
                    Food targetFood = boardControllerInstance.GetFoodByGridValues(_gridValueX, _gridValueY+1);
                    if (targetFood == null)
                    {
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY+1, _myFood);
                        SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX, _gridValueY+1));
                        SetGridValue(_gridValueX, _gridValueY+1);
                        ChangeGridByDirection(direction);//TESTTT
                        isChanged = true;
                    }
                    else
                    {
                        //Control
                        if (_foodType==targetFood.GetFoodType())
                        {
                            DOTween.Kill(targetFood.transform);
                            Destroy(targetFood.gameObject);//Bunu Kontrol Et
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY +1, _myFood);
                            SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX, _gridValueY+1));
                            SetGridValue(_gridValueX, _gridValueY+1);
                            boardControllerInstance.IncreaseEmptyCount();
                            SetFoodType(_foodType);
                            isChanged = true;
                        }
                        else
                        {
                            //STOP
                        }
                    }
                }
                else
                {
                    //STOP
                }

                break;
            case Directions.Down:
                if (_gridValueX != 3)
                {
                    Food targetFood = boardControllerInstance.GetFoodByGridValues(_gridValueX + 1, _gridValueY);
                    if (targetFood == null)
                    {
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                        boardControllerInstance.ChangeGridValue(_gridValueX + 1, _gridValueY, _myFood);
                        SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX + 1, _gridValueY));
                        SetGridValue(_gridValueX + 1, _gridValueY);
                        ChangeGridByDirection(direction);//TESTTT
                        isChanged = true;
                    }
                    else
                    {
                        //Control
                        if (_foodType == targetFood.GetFoodType())
                        {
                            DOTween.Kill(targetFood.transform);
                            Destroy(targetFood.gameObject);//Bunu Kontrol Et
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                            boardControllerInstance.ChangeGridValue(_gridValueX + 1, _gridValueY, _myFood);
                            SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX + 1, _gridValueY));
                            SetGridValue(_gridValueX + 1, _gridValueY);
                            boardControllerInstance.IncreaseEmptyCount();
                            SetFoodType(_foodType);
                            isChanged = true;
                        }
                        else
                        {
                            //STOP
                        }
                    }
                }
                else
                {
                    //STOP
                }

                break;
            case Directions.Left:
                if (_gridValueY != 0)
                {
                    Food targetFood = boardControllerInstance.GetFoodByGridValues(_gridValueX, _gridValueY -1);
                    if (targetFood == null)
                    {
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                        boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY - 1, _myFood);
                        SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX, _gridValueY - 1));
                        SetGridValue(_gridValueX, _gridValueY - 1);
                        ChangeGridByDirection(direction);//TESTTT
                        isChanged = true;
                    }
                    else
                    {
                        //Control
                        if (_foodType == targetFood.GetFoodType())
                        {
                            DOTween.Kill(targetFood.transform);
                            Destroy(targetFood.gameObject);//Bunu Kontrol Et
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY, null);
                            boardControllerInstance.ChangeGridValue(_gridValueX, _gridValueY - 1, _myFood);
                            SetPositions(boardControllerInstance.GetVectorByGridValues(_gridValueX, _gridValueY - 1));
                            SetGridValue(_gridValueX, _gridValueY - 1);
                            boardControllerInstance.IncreaseEmptyCount();
                            SetFoodType(_foodType);
                            isChanged = true;
                        }
                        else
                        {
                            //STOP
                        }
                    }
                }
                else
                {
                    //STOP
                }

                break;
            default:
                break;
        }
        if (isChanged)
        {
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    public FoodTypes GetFoodType()
    {
        return _foodType;
    }

    public void SetFoodType(FoodTypes foodType)
    {
        int foodTypeIndexValue = (int)foodType;
        _foodType = (FoodTypes)(foodTypeIndexValue + 1);
        ChangeSpriteByFoodType(foodTypeIndexValue + 1);
        if (foodTypeIndexValue==0)
        {
            boardControllerInstance.SetScore(boardControllerInstance.GetScore() + 2);
        }
        else
        {
            boardControllerInstance.SetScore(boardControllerInstance.GetScore() + (foodTypeIndexValue * 2));
        }
        
        if (_foodType==FoodTypes.FoodType10)
        {
            Debug.Log("Kazandınız");
            GameManager.Instance().LevelUp();
        }
    }

    public int CanMove()
    {
        int gridCount = boardControllerInstance.GetGridCount();
        bool canIMove = false;
        bool isCompletedGridsX = false;

        int fakeGridValueX = _gridValueX-1;
        int fakeGridValueY = _gridValueY;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (fakeGridValueX>=0 && fakeGridValueX< gridCount && fakeGridValueY>=0 && fakeGridValueY<gridCount)
                {
                    Food targetFood = boardControllerInstance.GetFoodByGridValues(fakeGridValueX, fakeGridValueY);
                    if (targetFood.GetFoodType()==_foodType)
                    {
                        canIMove = true;
                        break;
                    }
                }
                if (!isCompletedGridsX)
                {
                    fakeGridValueX += 2;
                }
                else
                {
                    fakeGridValueY += 2;
                }
               
            }
            isCompletedGridsX = true;
            fakeGridValueX = _gridValueX;
            fakeGridValueY -= 1;
        }

        if (canIMove)
        {
            return 1;
        }
        else
        {
            return 0;
        }

        
    }



}
