using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MenuSceneManager : NetworkBehaviour
{
    private void Awake()
    {
        NetworkManager.OnServerStarted += LoadLobby;
    }

    void LoadLobby()
    {
        NetworkManager.SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
