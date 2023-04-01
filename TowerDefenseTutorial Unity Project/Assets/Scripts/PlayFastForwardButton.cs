using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFastForwardButton : MonoBehaviour
{
    public static bool fastForwardEnabled = false;
    public GameObject playImage;
    public GameObject fastForwardImage;

    private void Start()
    {
        playImage.SetActive(true);
        fastForwardImage.SetActive(false);
        fastForwardEnabled = false;
        Time.timeScale = 1.0f;
    }

    public void PlayFastForward()
    {
        if (!WaveSpawner.inWave)
        {
            WaveSpawner.inWave = true;
            WaveSpawner.currentWave++;

            if (fastForwardEnabled)
            {
                ToggleFastForwardImage();
            }
        } else
        {
            ToggleFastForward();
        }
    }

    public void ToggleFastForward()
    {
        ToggleFastForwardImage();

        if (fastForwardEnabled)
        {
            Time.timeScale = 1;
            fastForwardEnabled = false;
        } else
        {
            Time.timeScale = 2;
            fastForwardEnabled = true;
        }
    }

    public void ToggleFastForwardImage()
    {
        playImage.SetActive(!playImage.activeSelf);
        fastForwardImage.SetActive(!fastForwardImage.activeSelf);
    }
}
