using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseUI : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject UI;
    public static bool isPaused = false;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    private void Start()
    {
        isPaused = false;
    }

    public void TogglePause()
    {
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            isPaused = true;
            Time.timeScale = 0f;
        } else
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        TogglePause();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        TogglePause();
        sceneFader.FadeTo(menuSceneName);
    }
}
