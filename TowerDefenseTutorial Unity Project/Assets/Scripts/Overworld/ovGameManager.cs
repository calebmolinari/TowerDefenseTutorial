using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ovGameManager : MonoBehaviour
{
    public PlayerData playerData;
    public TurretUnlockData unlockData;
    [SerializeField] SceneFader sceneFader;
    [SerializeField] GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerData.ovHealth = playerData.ovMaxHealth;
        LoadGame();
    }

    void Update()
    {
        playerData.ovHealth -= playerData.ovHealthDrainRate * Time.deltaTime;
        if (playerData.ovHealth <= 0)
        {
            //Start encounter
            SaveGame();
            int range = Random.Range(1, 2);
            sceneFader.FadeTo("Level" + range.ToString());
            this.enabled = false;
        }

        if (PauseUI.isPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1f;
        }
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(playerData);
        SaveSystem.SaveOverworld(playerObj);
        SaveSystem.SaveUnlocks(unlockData);
    }

    public void LoadGame()
    {
        SavePlayer player = SaveSystem.LoadPlayer();
        SaveOverworld overworld = SaveSystem.LoadOverworld();
        SaveUnlocks unlocks = SaveSystem.LoadUnlocks();

        //Unlock data
        unlockData.missile = unlocks.missile;
        unlockData.laser = unlocks.laser;

        //Player data
        playerData.health = player.health;
        playerData.maxHealth = player.maxHealth;

        //Overworld data
        Vector3 position;
        position.x = overworld.ovPosition[0];
        position.y = overworld.ovPosition[1];
        position.z = overworld.ovPosition[2];
        playerObj.transform.position = position;
    }
}
