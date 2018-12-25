using UnityEngine;

public class ResumeButtonHandler : MonoBehaviour
{
    public GameObject PausedScreen;

    public void OnResumButtonClicked()
    {
        PausedScreen.SetActive(false);
        GameController.Instance.GamePaused = false;
        Time.timeScale = 1;
    }
}
