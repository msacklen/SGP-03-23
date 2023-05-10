using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    [SerializeField] TMP_Text timerText;
    NetworkVariable<float> time = new NetworkVariable<float>(3600f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Update()
    {
        if (IsHost) time.Value -= Time.deltaTime;
        string timeText;

        float roundedTime = Mathf.Round(time.Value);
        float fSeconds = roundedTime % 60;
        string sSeconds = fSeconds.ToString();
        float fMinutes = (roundedTime - fSeconds) / 60;
        string sMinutes = fMinutes.ToString();

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
