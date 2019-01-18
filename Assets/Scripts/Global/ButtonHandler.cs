﻿using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void CloseApplicationClicked()
    {
        SceneHandler.Instance.CloseApplication();
    }

    public void StartGameClicked(string character)
    {
        SceneHandler.Instance.StartGame(character);
    }

    public void HighScoreViewClicked()
    {
        SceneHandler.Instance.ShowHighScore();
    }

    public void HelpViewClicked()
    {
        SceneHandler.Instance.ShowHelp();
    }

    public void BackToMainMenuClicked(int indexOfCurrentScene)
    {
        SceneHandler.Instance.ShowMainMenu(indexOfCurrentScene);
    } 

}