using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        UpdateGameTimer();
    }

    private void UpdateGameTimer()
    {
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0");
    }
}
