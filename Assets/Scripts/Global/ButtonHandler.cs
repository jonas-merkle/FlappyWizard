using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void CloseApplicationClicked()
    {
        SceneHandler.Instance.CloseApplication();
    }

    public void StartGameClicked(GameObject character)
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

    public void BackToMainMenuClicked(string nameOfCurrentScene)
    {
        SceneHandler.Instance.ShowMainMenu(nameOfCurrentScene);
    } 

}
