using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName;
    public GameManager gameManager;
   
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void Continue()
    {
        gameManager.SaveGame();
        sceneFader.FadeTo("Overworld");
    }
}
