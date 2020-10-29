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
    [SerializeField]
    private GameObject _levelUpPanel;
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private List<Button> _levelButtons = new List<Button>();
    [SerializeField]
    private List<Sprite> _levelButtonSelectedImages = new List<Sprite>();
    [SerializeField]
    private List<Sprite> _levelButtonLockedImages = new List<Sprite>();
    [SerializeField]
    private List<Sprite> _levelButtonOpenedImages = new List<Sprite>();


    private void Start()
    {
        LevelButtonsControl();
    }

    public void OpenPanel(GameObject panel)
    {
        UIManagerSystem.Instance().SetCurrentPanel(panel, true);
    }

    public void ClosePanel(GameObject panel)
    {
        UIManagerSystem.Instance().SetCurrentPanel(panel, false);
    }

    public void ChangeMap(int mapNumber)
    {
        GameManager.Instance().SelectMap(mapNumber);
        _levelButtons[mapNumber].GetComponent<Image>().sprite = _levelButtonSelectedImages[mapNumber];

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

    public void ChangeScoreText(int score)
    {
        _scoreText.text = "Score : "+score.ToString();
    }

    public void DisableLevelButtons()
    {
        int completedLevelCount = LevelController.Instance().GetCompletedLevelCount();
        for (int i = completedLevelCount+1; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].enabled = false;
        }
    }
    private void LevelButtonsControl()
    {
        int completedLevelCount = LevelController.Instance().GetCompletedLevelCount();
        _levelButtons[0].GetComponent<Image>().sprite = _levelButtonSelectedImages[0];
        for (int i = completedLevelCount+1; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].GetComponent<Image>().sprite = _levelButtonLockedImages[i];
        }
        DisableLevelButtons();
    }

    public void EnableLevelButton(int levelNumber)
    {
        _levelButtons[levelNumber].enabled = true;
    }

    public void ShowLevelUpPanel()
    {
        _levelUpPanel.SetActive(true);
    }

    public void CloseLevelUpPanel()
    {
        _levelUpPanel.SetActive(false);
    }
    

}
