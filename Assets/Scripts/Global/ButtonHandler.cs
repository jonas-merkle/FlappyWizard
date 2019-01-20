using UnityEngine;

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

    public void BackToMainMenuClicked()
    {
        SceneHandler.Instance.ShowMainMenu();
    }

    public void ResumButtonClicked(GameObject pausedScreen)
    {
        pausedScreen.SetActive(false);
        GameControl.Instance.GamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void SaveButtonClicked()
    {
        HighScoreHandler.Instance.AddScore(GameControl.Instance.PlayerNameInputText.text, int.Parse(GameControl.Instance.ScoreText.text));

        BackToMainMenuClicked();
    }

    public void ResetButtonClicked()
    {
        HighScoreHandler.Instance.ResetHighScore();

        HighScoreViewClicked();
    }

}
