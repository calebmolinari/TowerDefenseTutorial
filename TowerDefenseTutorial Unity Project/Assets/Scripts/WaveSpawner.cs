using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveData waveData;
    [SerializeField] Transform spawn;
    public PlayFastForwardButton playFastForwardButton;
    EnemyPooler enemyPooler;
    public GameManager gameManager;
    
    float spawnTimer = 0f;
    int countSpawns = 0;
    public static int currentWave = 0;
    public static bool inWave = false;
    public static int enemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyPooler = EnemyPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

       
        //Wave spawning and level ending logic
        if (inWave)
        {
            //If there are still enemies to spawn
            if (countSpawns < waveData.waveSize[currentWave - 1])
            {
                //If enough time has passed to spawn next enemy
                if (spawnTimer >= waveData.timeBetweenEnemies)
                {
                    //Spawn pooled enemy
                    enemyPooler.SpawnFromPool(waveData.enemyTags[0], spawn.position, Quaternion.identity);
                    countSpawns++;
                    enemiesAlive++;
                    spawnTimer = 0f;
                }
                return;
            }
            //Should wave end?
            if (enemiesAlive == 0)
            {
                //Wave is over
                inWave = false;
                countSpawns = 0;
                //Should play button image be toggled?
                CheckToggleButton();
                //Should game end?
                if (currentWave == waveData.waveSize.Length)
                {
                    //Win level
                    gameManager.WinLevel();
                    this.enabled = false;
                }
            }
        }
    }

    

   private void CheckToggleButton()
    {
        if (PlayFastForwardButton.fastForwardEnabled)
        {
            playFastForwardButton.ToggleFastForwardImage();
        }
    }

}
