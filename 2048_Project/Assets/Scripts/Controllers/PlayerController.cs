using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Directions _direction;

    private bool _isEnd = true;

    Vector3 startPos;

    private void Update()
    {
        if (Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Began)
            {
                startPos= touch.position;
            }
            else if (touch.phase==TouchPhase.Moved)
            {
                if ((startPos.y+100f)<touch.position.y && _isEnd)
                {
                    _direction = Directions.Up;
                    Debug.Log(_direction);
                    _isEnd = false;
                    BoardController boardController = FindObjectOfType<BoardController>();
                    boardController.ChangeGridsFromDirection(_direction);
                }
                else if ((startPos.y- 100f) > (touch.position.y) && _isEnd)
                {
                    _direction = Directions.Down;
                    Debug.Log(_direction);
                    _isEnd = false;
                    BoardController boardController = FindObjectOfType<BoardController>();
                    boardController.ChangeGridsFromDirection(_direction);
                }
                else if ((startPos.x + 100f) < (touch.position.x) && _isEnd)
                {
                    _direction = Directions.Right;
                    Debug.Log(_direction);
                    _isEnd = false;
                    BoardController boardController = FindObjectOfType<BoardController>();
                    boardController.ChangeGridsFromDirection(_direction);
                }
                else if ((startPos.x - 100f) > (touch.position.x) && _isEnd)
                {
                    _direction = Directions.Left;
                    Debug.Log(_direction);
                    _isEnd = false;
                    BoardController boardController = FindObjectOfType<BoardController>();
                    boardController.ChangeGridsFromDirection(_direction);
                }
            }
            else if (touch.phase==TouchPhase.Ended)
            {
                _isEnd = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Directions.Right;
            _isEnd = true;
            BoardController boardController = FindObjectOfType<BoardController>();
            boardController.ChangeGridsFromDirection(_direction);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Directions.Left;
            _isEnd = true;
            BoardController boardController = FindObjectOfType<BoardController>();
            boardController.ChangeGridsFromDirection(_direction);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Directions.Up;
            _isEnd = true;
            BoardController boardController = FindObjectOfType<BoardController>();
            boardController.ChangeGridsFromDirection(_direction);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Directions.Down;
            _isEnd = true;
            BoardController boardController = FindObjectOfType<BoardController>();
            boardController.ChangeGridsFromDirection(_direction);
        }
    }

}
