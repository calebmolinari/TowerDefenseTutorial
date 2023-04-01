using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    public TurretUnlockData unlockData;
    public static bool gameEnded;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
        gameOverUI.SetActive(false);
        completeLevelUI.SetActive(false);
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if (playerData.health <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        completeLevelUI.SetActive(true);
        gameEnded = true;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(playerData);
    }

    public void LoadGame()
    {
        SavePlayer player = SaveSystem.LoadPlayer();
        SaveUnlocks unlocks = SaveSystem.LoadUnlocks();

        //Player data
        playerData.health = player.health;
        playerData.maxHealth = player.maxHealth;

        //Unlock data
        unlockData.missile = unlocks.missile;
        unlockData.laser = unlocks.laser;
    }
}
