using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;
using System.Net;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class LobbySceneManager : NetworkBehaviour
{
    [SerializeField] GameObject sceneSwitchCanvas;
    [SerializeField] TMP_Text textIP;

    private void Start()
    {
        if (!IsHost) sceneSwitchCanvas.SetActive(false);
        else
        {
            IPAddress[] localIPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            string hostIP = localIPs.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            textIP.text = hostIP;
        }
    }

    public void LoadApartment()
    {
        NetworkManager.SceneManager.LoadScene("Apartment", LoadSceneMode.Single);
    }
}
