using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public TextMeshProUGUI roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int round = 0;
        roundsText.text = "0";

        while (round < WaveSpawner.currentWave)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
