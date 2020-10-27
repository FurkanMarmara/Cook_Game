using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _tryAgainPanel;
    [SerializeField]
    private GameObject _inGamePanel;

    public void OpenPanel(GameObject panel)
    {
        UIManagerSystem.Instance().SetCurrentPanel(panel, true);
    }

    public void ClosePanel(GameObject panel)
    {
        UIManagerSystem.Instance().SetCurrentPanel(panel, false);
    }

    public void StartToGame()
    {
        UIManagerSystem.Instance().SetCurrentPanel(_startPanel, false);
        GameManager.Instance().StartToGame();
        _inGamePanel.SetActive(true);
    }

    public void TryAgain()
    {
        UIManagerSystem.Instance().SetCurrentPanel(_tryAgainPanel, false);
        BoardController.Instance().TryAgain(true);
    }

    public void GameOver()
    {
        UIManagerSystem.Instance().SetCurrentPanel(_tryAgainPanel, true);
    }

    public void BackToMainMenu()
    {
        _inGamePanel.SetActive(false);
        GameManager.Instance().TryAgain(false);
        OpenPanel(_startPanel);
    }

}
