﻿using UnityEngine;

public class ResumeButtonHandler : MonoBehaviour
{
    public GameObject PausedScreen;

    public void OnResumButtonClicked()
    {
        PausedScreen.SetActive(false);
        GameControl.Instance.GamePaused = false;
        Time.timeScale = 1;
    }
}