using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    float time = 3600f;

    private void Update()
    {
        time -= Time.deltaTime;
        string timeText;

        float fMinutes = time % 60;
        string sMinutes = fMinutes.ToString();
        float fSeconds = Mathf.Round(time - (fMinutes * 60));
        string sSeconds = fSeconds.ToString();

        if (fSeconds % 2 == 0)
        {
            timeText = sMinutes + ":" + sSeconds;
        }
        else
        {
            timeText = sMinutes + " " + sSeconds;
        }

        timerText.text = timeText;
    }
}
