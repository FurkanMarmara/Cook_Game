using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    #region Singleton
    private static LevelController _instance;

    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }
    #endregion

    private int _completedLevelCount = 0;

    private void Start()
    {
        _completedLevelCount = PlayerPrefs.GetInt("CompletedLevelCount");
    }

    public static LevelController Instance()
    {
        return _instance;
    }


    public int GetCompletedLevelCount()
    {
        return _completedLevelCount;
    }

    public void SetCompletedLevelCount(int completedLevelCount)
    {
        _completedLevelCount = completedLevelCount;
        PlayerPrefs.SetInt("CompletedLevelCount", completedLevelCount);
    }
}
