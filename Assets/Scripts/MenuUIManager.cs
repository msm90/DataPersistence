using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] TMP_Text playerNameTextField;
    [SerializeField] TMP_Text highscoreTextField;
    [SerializeField] TMP_Text highscoreValueTextField;
    public void LoadGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
    public void SetPlayerName()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.PlayerName = this.playerNameInputField.text;
        }
        this.playerNameTextField.text = string.Concat("Currently playing: ", this.playerNameInputField.text);
    }
    private void Start() {
        if (MenuManager.Instance != null && MenuManager.Instance.PlayerName != null)
            if (this.playerNameTextField)
                this.playerNameTextField.text = string.Concat("Currently playing: ", MenuManager.Instance.PlayerName);
        if (MenuManager.Instance != null && MenuManager.Instance.HighScore == 0)
            if (this.highscoreTextField)
                this.highscoreTextField.gameObject.SetActive(false);
        if (MenuManager.Instance != null && MenuManager.Instance.HighScore > 0)
        {
            if (this.highscoreTextField)
                this.highscoreTextField.gameObject.SetActive(true);
            SetHighScoreValueText();
        }
    }

    public void SetHighScoreValueText()
    {
        if (this.highscoreTextField)
            this.highscoreTextField.gameObject.SetActive(true);
        this.highscoreValueTextField.text = string.Concat(MenuManager.Instance.HighScorePlayer, ":", MenuManager.Instance.HighScore);
    }

    public void Exit()
    {
            MenuManager.Instance.SaveHighscore();
            if (Application.isEditor)
                EditorApplication.ExitPlaymode();
            else
                Application.Quit();
    }
}
