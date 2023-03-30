using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class LobbySceneManager : NetworkBehaviour
{
    [SerializeField] GameObject sceneSwitchCanvas;

    private void Start()
    {
        if (!IsHost) sceneSwitchCanvas.SetActive(false);
    }

    public void LoadApartment()
    {
        NetworkManager.SceneManager.LoadScene("Apartment", LoadSceneMode.Single);
    }
}
