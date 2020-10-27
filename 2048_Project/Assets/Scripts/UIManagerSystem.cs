using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerSystem : MonoBehaviour
{

    private static UIManagerSystem _instance;

    #region Singleton
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(_instance);
            }
        }
    }
    #endregion

    public static UIManagerSystem Instance()
    {
        return _instance;
    }


    [SerializeField]
    private GameObject _currentPanel;
    [SerializeField]
    private GameObject _previousPanel;
    [SerializeField]
    private List<GameObject> _openedPanels = new List<GameObject>();

    private void Start()
    {
        _openedPanels.Add(_currentPanel);
    }

    private void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    private void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void SetCurrentPanel(GameObject panel, bool isOpen)
    {
        if (isOpen)
        {
            if (_openedPanels.Count<=1)
            {
                _openedPanels.Add(panel);
                _currentPanel = _openedPanels[_openedPanels.Count - 1];
                _previousPanel = null;
                OpenPanel(_currentPanel);
            }
            else
            {
                _openedPanels.Add(panel);
                _currentPanel = _openedPanels[_openedPanels.Count - 1];
                _previousPanel = _openedPanels[_openedPanels.Count - 2];
                OpenPanel(_currentPanel);
                ClosePanel(_previousPanel);
            }
            
        }
        else
        {
            ClosePanel(_currentPanel);
            _openedPanels.Remove(panel);
            if (_openedPanels.Count>1)
            {
                _currentPanel = _openedPanels[_openedPanels.Count - 1];
                OpenPanel(_currentPanel);
                if (_openedPanels.Count > 1)
                {
                    _previousPanel = _openedPanels[_openedPanels.Count - 2];
                }
                else
                {
                    _previousPanel = null;
                }
            }
            
        }
    }


}
